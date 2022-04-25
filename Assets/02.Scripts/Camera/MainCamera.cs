using DG.Tweening;
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
    public static Transform Transform { get { return Camera.transform.parent; } }
    public static Transform Target { get; set; }
    public static Vector3 Offset { get; set; } = Camera.transform.position;

    private static Camera _camera;
    private static Bounds _bounds;
    private static Tweener _tweener;

    public static Bounds OrthographicBounds(float offset = 0)
    {
        float cameraHeight = (Camera.orthographicSize + offset) * 2;
        float screenAspect = (float)Screen.width / Screen.height;

        if (_bounds == null) _bounds = new Bounds();

        _bounds.center = Transform.position;
        _bounds.size = new Vector3(cameraHeight * screenAspect, cameraHeight, 0);

        return _bounds;
    }

    public static void Shake()
    {
        if (_tweener == null || !_tweener.IsActive())
        {
            _tweener = Camera.DOShakePosition(0.1f, .3f, 1, 9f);
        }
    }

    public static void Follow()
    {
        if (Target != null) Transform.position = Target.position + Offset;
    }
}
