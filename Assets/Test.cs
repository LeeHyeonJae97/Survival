using KgmSlem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Test : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log($"{name} Awake");
    }

    private void Start()
    {
        Debug.Log($"{name} Start");
    }

    private void OnEnable()
    {
        Debug.Log($"{name} OnEnable");
    }

    private void OnDisable()
    {
        Debug.Log($"{name} OnDisable");
    }
}
