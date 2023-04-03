using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct QuadtreeNode<T> where T : IQuadtreeObject
{
    private const int MAX_OBJECTS = 8;

    private Vector2 _center;
    private Vector2 _size;
    private int _depth;
    private int _minDepth;
    private Bounds _bounds;
    private List<T> _objects;
    private QuadtreeNode<T>[] _children;

    public QuadtreeNode(Vector2 center, Vector2 size, int depth, int minDepth)
    {
        _center = center;
        _size = size;
        _depth = depth;
        _minDepth = minDepth;

        _bounds = new Bounds(_center, new Vector3(_size.x, _size.y, 0.2f));

        _objects = new List<T>();
        _children = null;
    }

    public bool Add(T obj)
    {
        bool contains = _bounds.Contains(obj.Bounds);

        if (contains)
        {
            SubAdd(obj);
        }
        return contains;
    }

    public bool Remove(T obj)
    {
        for (int i = 0; i < _objects.Count; i++)
        {
            if (_objects[i].Equals(obj))
            {
                _objects.RemoveAt(i);

                if (_children != null)
                {
                    Merge();
                    return true;
                }
            }
        }

        if (_children != null)
        {
            for (int i = 0; i < _children.Length; i++)
            {
                if (_children[i].Remove(obj)) return true;
            }
        }

        return false;
    }

    public bool IsColliding(ref Bounds bounds)
    {
        if (!_bounds.Intersects(bounds)) return false;

        if (_children == null)
        {
            foreach (var obj in _objects)
            {
                if (obj.Bounds.Intersects(bounds)) return true;
            }
        }
        else
        {
            foreach (var child in _children)
            {
                if (child.IsColliding(ref bounds)) return true;
            }
        }

        return false;
    }

    public bool IsColliding(ref Ray ray, float maxDistance)
    {
        if (!_bounds.IntersectRay(ray, out var distance) || distance > maxDistance) return false;

        if (_children == null)
        {
            foreach (var obj in _objects)
            {
                if (obj.Bounds.IntersectRay(ray, out distance) && distance <= maxDistance) return true;
            }
        }
        else
        {
            foreach (var child in _children)
            {
                if (child.IsColliding(ref ray, maxDistance)) return true;
            }
        }

        return false;
    }

    public bool IsColliding(Vector3 center, float radius)
    {
        if (!_bounds.Intersects(center, radius)) return false;

        if (_children == null)
        {
            foreach (var obj in _objects)
            {
                if (obj.Bounds.Intersects(center, radius)) return true;
            }
        }
        else
        {
            foreach (var child in _children)
            {
                if (child.IsColliding(center, radius)) return true;
            }
        }

        return false;
    }

    public void GetColliding(List<T> colliding, ref Bounds bounds)
    {
        if (!_bounds.Intersects(bounds)) return;

        if (_children == null)
        {
            foreach (var obj in _objects)
            {
                if (obj.Bounds.Intersects(bounds))
                {
                    colliding.Add(obj);
                }
            }
        }
        else
        {
            foreach (var child in _children)
            {
                child.GetColliding(colliding, ref bounds);
            }
        }
    }

    public void GetColliding(List<T> colliding, ref Ray ray, float maxDistance)
    {
        if (!_bounds.IntersectRay(ray, out var distance) || distance > maxDistance) return;

        if (_children == null)
        {
            foreach (var obj in _objects)
            {
                if (obj.Bounds.IntersectRay(ray, out distance) && distance < maxDistance)
                {
                    colliding.Add(obj);
                }
            }
        }
        else
        {
            foreach (var child in _children)
            {
                child.GetColliding(colliding, ref ray, maxDistance);
            }
        }
    }

    public void GetColliding(List<T> colliding, Vector3 center, float radius)
    {
        if (!_bounds.Intersects(center, radius)) return;

        if (_children == null)
        {
            foreach (var obj in _objects)
            {
                if (obj.Bounds.Intersects(center, radius))
                {
                    colliding.Add(obj);
                }
            }
        }
        else
        {
            foreach (var child in _children)
            {
                child.GetColliding(colliding, center, radius);
            }
        }
    }

    private void SubAdd(T obj)
    {
        if (_children != null)
        {
            var childIndex = FindChild(obj.Bounds.center);

            _children[childIndex].SubAdd(obj);
        }
        else
        {
            if (_objects.Count < MAX_OBJECTS || _depth >= _minDepth)
            {
                _objects.Add(obj);
            }
            else
            {
                Split();

                var childIndex = FindChild(obj.Bounds.center);

                _children[childIndex].SubAdd(obj);
            }
        }
    }

    private void Split()
    {
        var quarter = _size / 4f;
        var half = _size / 2f;

        _children = new QuadtreeNode<T>[4];

        _children[0] = new QuadtreeNode<T>(_center + new Vector2(-quarter.x, quarter.y), half, _depth + 1, _minDepth);
        _children[1] = new QuadtreeNode<T>(_center + new Vector2(quarter.x, quarter.y), half, _depth + 1, _minDepth);
        _children[2] = new QuadtreeNode<T>(_center + new Vector2(-quarter.x, -quarter.y), half, _depth + 1, _minDepth);
        _children[3] = new QuadtreeNode<T>(_center + new Vector2(quarter.x, -quarter.y), half, _depth + 1, _minDepth);

        while (_objects.Count > 0)
        {
            var childIndex = FindChild(_objects[0].Bounds.center);

            _children[childIndex].SubAdd(_objects[0]);
            _objects.RemoveAt(0);
        }

        _objects = null;
    }

    private int FindChild(Vector3 center)
    {
        for (int i = 0; i < _children.Length; i++)
        {
            if (_children[i]._bounds.Contains(center))
            {
                return i;
            }
        }
        return -1;
    }

    private void Merge()
    {
        if (!IsNeedToBeMerged()) return;

        _objects = new List<T>();

        foreach (var child in _children)
        {
            _objects.AddRange(child._objects);
        }

        _children = null;
    }

    private bool IsNeedToBeMerged()
    {
        if (_children == null) return false;

        foreach (var child in _children)
        {
            if (child._children != null) return false;
        }

        return true;
    }

#if UNITY_EDITOR
    public void DrawBounds()
    {
        if (_children == null)
        {
            Gizmos.DrawWireCuboid(_bounds);
        }
        else
        {
            foreach (var child in _children)
            {
                child.DrawBounds();
            }
        }
    }
#endif
}
