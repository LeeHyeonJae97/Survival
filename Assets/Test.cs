using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Test : MonoBehaviour
{
    private void Start()
    {
        transform.position = transform.position.SetY(2.5f);
    }
}
