using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadSpawner : MonoBehaviour
{
    [SerializeField] private int _count;
    [SerializeField] private Quad _prefab;
    [SerializeField] private float _weight;
    [SerializeField] private QuadtreeSettings _settings;
    private Quad[] _quads;
    private List<Quad> _neighbors;
    private Quadtree<Quad> _quadTree;

    private void Start()
    {
        _quads = new Quad[_count];
        _neighbors = new List<Quad>();
        _quadTree = new Quadtree<Quad>(_settings.Center, _settings.Size, _settings.MinDepth);

        for (int i = 0; i < _count; i++)
        {
            var hs = _settings.Size / 2;
            var position = _settings.Center+ new Vector2(Random.Range(-hs.x, hs.x), Random.Range(-hs.y, hs.y));
            var rotation = Quaternion.identity;

            _quads[i] = Instantiate(_prefab, position, rotation);
        }
    }

    private void Update()
    {
        UpdateQuadTree();
        UpdateQuads();

        void UpdateQuadTree()
        {
            _quadTree.Reset();

            foreach (var quad in _quads)
            {
                _quadTree.Add(quad);
            }
        }

        void UpdateQuads()
        {
            foreach (var quad in _quads)
            {
                var direction = Vector3.zero;

                // toward center
                direction += MoveTowardTarget(quad);

                // backward from other
                direction += MoveBackwardFromOther(quad);

                quad.Direction = direction.normalized;
            }

            Vector3 MoveTowardTarget(Quad quad)
            {
                return transform.position - quad.transform.position;
            }

            Vector3 MoveBackwardFromOther(Quad quad)
            {
                var direction = Vector3.zero;

                _neighbors.Clear();
                _quadTree.GetColliding(_neighbors, quad.transform.position, quad.Sight);

                foreach (var other in _neighbors)
                {
                    if (quad != other)
                    {
                        var dir = quad.transform.position - other.transform.position;
                        var dst = dir.magnitude;

                        direction += dir.normalized / dst * _weight;
                    }
                }

                return direction * _neighbors.Count;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (_quadTree != null)
        {
            _quadTree.DrawBounds();
        }
    }
}
