using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Initializer : MonoBehaviour
{
    private void Start()
    {
        SceneManager.LoadScene("Common");
        SceneManager.LoadSceneAsync("Title", LoadSceneMode.Additive);
    }
}
