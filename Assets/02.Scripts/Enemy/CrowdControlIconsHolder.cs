using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdControlIconsHolder : MonoBehaviour
{
    private void OnTransformChildrenChanged()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).localPosition = new Vector3(-0.2f + 0.3f * i, 0.6f);
        }
    }
}
