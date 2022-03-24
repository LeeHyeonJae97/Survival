using KgmSlem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private RouletteUI _rouletteUI;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _rouletteUI.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            _rouletteUI.SetActive(false);
        }
    }
}
