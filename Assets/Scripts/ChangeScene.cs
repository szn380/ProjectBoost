using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {
    int countDown = 200;
	int sceneCounter = 0;
	int repeatLevel = 3; // scene number to start repeating on death rather than starting over

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (countDown-- <= 0)
        {
			sceneCounter = SceneManager.GetActiveScene().buildIndex;
			if (sceneCounter >= repeatLevel)
            {
				SceneManager.LoadScene(sceneCounter);
			}
			else
            {
				SceneManager.LoadScene(0);
			}	
        }
    }
}
