using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct QuadtreeSettings
{
    public Vector2 Center => _center;
    public Vector2 Size => _size;
    public int MinDepth => _minDepth;

    [SerializeField] private Vector2 _center;
    [SerializeField] private Vector2 _size;
    [SerializeField] private int _minDepth;
}
