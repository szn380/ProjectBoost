using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {
	// This class is activated after a player has died
	// The level successfully completed is not the current level (the player died), but the prior level
	// The successfully completed level number is stored in the GameData class for later use
	// If in Flight School mode, the current level should be repeated (reloaded), as the player gets to 
	// try to complete it again.
	// In Play for Score mode, the next level is the get initials level/scene, given that the player has died

    int countDown = 200;   // number of frames to wait to transition to next scene/level
	int sceneCounter = 0;  // number of the current scene / level

	// Save the successfully completed level in GameData for later reference
	void Start () {
		GameData.playerFinalLevel = SceneManager.GetActiveScene().buildIndex-1;
	}
	
	// After delay, transition to next scene / level
	void Update () {
        if (countDown-- <= 0)
        {
			sceneCounter = SceneManager.GetActiveScene().buildIndex;
			if (GameData.flightSchool)
            {
				SceneManager.LoadScene(sceneCounter);  // reload same level/scene again
			}
			else if (sceneCounter > 1)
            {
				SceneManager.LoadScene(GameData.getInitialsLevelNum);  // load the get initials level
			}
			else
            {
				SceneManager.LoadScene(1);  // Play for Score, but did not even complete 1st level, try again
			}	
        }
    }
}
