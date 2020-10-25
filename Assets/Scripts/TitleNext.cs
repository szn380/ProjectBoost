using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleNext : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int sceneCounter = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(sceneCounter);
        }
    }
}
