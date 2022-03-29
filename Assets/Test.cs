using KgmSlem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Test : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Canvas _canvas;

    public void OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
    {
        Debug.Log($"{Vector2.zero} / {transform.position} / {_canvas.TransformScreenPoint(Vector2.zero)} / {Camera.main.ScreenToWorldPoint(Vector3.zero - new Vector3(0, 0, Camera.main.transform.position.z))}");
        //Debug.Log($"{eventData.position} / {transform.position} / {_canvas.TransformScreenPoint(eventData.position)}");
    }
}
