using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private IEnumerator _testCor;

    private void Start()
    {
        StartCoroutine(CoTest());
        StartCoroutine(CoTest());
        StartCoroutine(CoTest());
        StartCoroutine(CoTest());
        StartCoroutine(CoTest());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ((WaitForSeconds)_testCor.Current).Reset();
        }
    }

    private IEnumerator CoTest()
    {
        int count = 5;
        while(count-- > 0)
        {
            Debug.Log(count);
            yield return WaitForSecondsFactory.Get(1f);
        }
    }
}
