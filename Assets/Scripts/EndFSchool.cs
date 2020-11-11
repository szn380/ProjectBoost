using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndFSchool : MonoBehaviour
{
    void Update()  // leave last Flight School Level back to Title level/scene
    {
        if (Input.GetKeyDown(KeyCode.Q))  // TBD Del ME
        {
            // SceneManager.LoadScene(0);
        }
    }

    public void processMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
