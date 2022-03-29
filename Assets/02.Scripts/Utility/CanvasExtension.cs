using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CanvasExtension
{
    public static Vector3 TransformScreenPoint(this Canvas canvas, Vector3 vec)
    {
        if (canvas.renderMode == RenderMode.ScreenSpaceOverlay)
        {
            Debug.LogWarning("it doesn't mean anything with overlay mode");
        }

        Debug.Log(((RectTransform)canvas.transform).sizeDelta);
        Debug.Log(((RectTransform)canvas.transform).rect.size);

        Vector3 transformed = canvas.transform.TransformPoint(vec);
        transformed -= canvas.transform.TransformDirection((Vector3)((RectTransform)canvas.transform).sizeDelta / 2 * canvas.transform.localScale.x);
        return transformed;
    }

    public static Vector2 InverseTransformDirection(this Canvas canvas, Vector3 vec)
    {
        return canvas.transform.InverseTransformDirection(vec);
    }
}
