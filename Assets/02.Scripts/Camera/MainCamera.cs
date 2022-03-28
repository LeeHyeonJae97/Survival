using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MainCamera
{
    public static Camera Camera
    {
        get
        {
            if (_camera == null || !_camera.CompareTag("MainCamera")) _camera = UnityEngine.Camera.main;
            return _camera;
        }
    }

    private static Camera _camera;
    private static Bounds _bounds;

    public static Bounds OrthographicBounds(float offset = 0)
    {
        float cameraHeight = (Camera.orthographicSize + offset) * 2;
        float screenAspect = (float)Screen.width / Screen.height;

        if (_bounds == null) _bounds = new Bounds();

        _bounds.center = Camera.transform.position;
        _bounds.size = new Vector3(cameraHeight * screenAspect, cameraHeight, 0);

        return _bounds;
    }
}
