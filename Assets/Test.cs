using KgmSlem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            UIFactory.Get<ConfirmUI>().Confirm("Hi?", () => Debug.Log("Accepted"));
        }
    }
}
