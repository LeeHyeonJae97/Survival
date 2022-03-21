using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CameraExtension
{
    // TODO :
    // camera's orthographicSize does not change during play,
    // so updating bounds' center can be enough
    public static void OrthographicBounds(this Camera camera, ref Bounds bounds, float offset = 0)
    {
        float cameraHeight = (camera.orthographicSize + offset) * 2;
        float screenAspect = (float)Screen.width / Screen.height;

        bounds.center = camera.transform.position;
        bounds.size = new Vector3(cameraHeight * screenAspect, cameraHeight, 0);
    }
}
