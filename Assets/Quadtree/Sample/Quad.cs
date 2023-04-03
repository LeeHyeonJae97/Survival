using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quad : MonoBehaviour, IQuadtreeObject
{
    public Bounds Bounds { get { return _renderer.bounds; } }
    public Vector3 Direction { get; set; }
    public float Sight => _sight;

    [SerializeField] private float _speed;
    [SerializeField] private float _sight;
    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponentInChildren<Renderer>(true);
    }

    private void Update()
    {
        transform.position += _speed * Time.deltaTime * Direction;
    }
}
