using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBounds
{
    private UnityEngine.Camera _camera;
    public Bounds _bounds;

    public CameraBounds(UnityEngine.Camera camera)
    {
        _camera = camera;
        _bounds = new Bounds();
    }

    public Vector2 RandomPointOnBounds(float offset = 0f)
    {
        _camera.OrthographicBounds(ref _bounds, offset);
        return _bounds.OnRandom();
    }
}
