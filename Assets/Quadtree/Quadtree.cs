using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quadtree<T> where T : IQuadtreeObject
{
    public int Count { get; private set; }

    [SerializeField] private Vector3 _center;
    [SerializeField] private Vector2 _size;
    [SerializeField] private int _minDepth;
    private QuadtreeNode<T> _root;
#if UNITY_EDITOR
    private Bounds _collidingBounds;
    private Ray _collidingRay;
    private Vector3 _collidingCenter;
    private float _collidingRadius;
    private List<T> _collidings;
#endif

    public Quadtree(Vector3 center, Vector2 size, int minDepth)
    {
        _center = center;
        _size = size;
        _minDepth = minDepth;

        _root = new QuadtreeNode<T>(_center, _size, 0, _minDepth);
    }

    public void Reset()
    {
        _root = new QuadtreeNode<T>(_center, _size, 0, _minDepth);
    }

    public bool Add(T obj)
    {
        var success = _root.Add(obj);

        if (success)
        {
            Count++;
        }
        return success;
    }

    public bool Remove(T obj)
    {
        bool removed = _root.Remove(obj);

        if (removed)
        {
            Count--;
        }
        return removed;
    }

    public bool IsColliding(Bounds bounds)
    {
        _collidingBounds = bounds;

        return _root.IsColliding(ref bounds);
    }

    public bool IsColliding(Vector3 origin, Vector3 direction, float maxDistance = float.MaxValue)
    {
        var ray = new Ray(origin, direction);

        _collidingRay = ray;

        return _root.IsColliding(ref ray, maxDistance);
    }

    public bool IsColliding(Ray ray, float maxDistance = float.MaxValue)
    {
        _collidingRay = ray;

        return _root.IsColliding(ref ray, maxDistance);
    }

    public bool IsColliding(Vector3 center, float radius)
    {
        _collidingCenter = center;
        _collidingRadius = radius;

        return _root.IsColliding(center, radius);
    }

    public void GetColliding(List<T> collidings, Bounds bounds)
    {
        if (collidings == null) return;

        _collidingBounds = bounds;
        _collidings = collidings;

        _root.GetColliding(collidings, ref bounds);
    }

    public void GetColliding(List<T> collidings, Vector3 origin, Vector3 direction, float maxDistance = float.MaxValue)
    {
        if (collidings == null) return;

        var ray = new Ray(origin, direction);

        _collidingRay = ray;
        _collidings = collidings;

        _root.GetColliding(collidings, ref ray, maxDistance);
    }

    public void GetColliding(List<T> collidings, Ray ray, float maxDistance = float.MaxValue)
    {
        if (collidings == null) return;

        _collidingRay = ray;
        _collidings = collidings;

        _root.GetColliding(collidings, ref ray, maxDistance);
    }

    public void GetColliding(List<T> collidings, Vector3 center, float radius)
    {
        if (collidings == null) return;

        _collidingCenter = center;
        _collidingRadius = radius;
        _collidings = collidings;

        _root.GetColliding(collidings, center, radius);
    }

#if UNITY_EDITOR
    public void DrawBounds()
    {
        _root.DrawBounds();

        Gizmos.DrawWireCuboid(_collidingBounds, Color.green);

        Gizmos.DrawRay(_collidingRay, Color.green);

        Gizmos.DrawWireCircle(_collidingCenter, _collidingRadius, Color.green);

        if (_collidings != null)
        {
            foreach (var colliding in _collidings)
            {
                Gizmos.DrawWireCuboid(colliding.Bounds, Color.red);
            }
        }
    }
#endif
}
