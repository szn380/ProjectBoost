using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleNext : MonoBehaviour
{
    public Text text;

    // on startup, check to see if the GameData top score list
    // needs to be updated (update if needed). Refresh the UI
    // top score list
    private void Start()
    {
        if (GameData.addPlayer)
        {
            updateTopScoreList();
        }
        print("Refresh UI Scores");
        updateUITopScoreList();
     }

    private void updateUITopScoreList()
        // Read top score list from GameData and build text string to update 
        // UI 
    {
        // Update UI with current list of top scores
        string text1, text2;
        text1 = string.Format("{0}  {1}\n{2}  {3}\n{4}  {5}\n{6}  {7}\n{8}  {9}\n",
            GameData.playerLevel[0], GameData.playerNames[0],
            GameData.playerLevel[1], GameData.playerNames[1],
            GameData.playerLevel[2], GameData.playerNames[2],
            GameData.playerLevel[3], GameData.playerNames[3],
            GameData.playerLevel[4], GameData.playerNames[4]);
        text2 = string.Format("{0}  {1}\n{2}  {3}\n{4}  {5}\n{6}  {7}\n{8}  {9}\n",
            GameData.playerLevel[5], GameData.playerNames[5],
            GameData.playerLevel[6], GameData.playerNames[6],
            GameData.playerLevel[7], GameData.playerNames[7],
            GameData.playerLevel[8], GameData.playerNames[8],
            GameData.playerLevel[9], GameData.playerNames[9]);
        text.text = text1 + text2;
    }

    // Sort the players scores / initials into the top game list
    private static void updateTopScoreList()
    {
        int i = 0;
        string savedInitials, savedInitials1;
        int savedLevel, savedLevel1;

        // identify where to insert score
        while ((i < 10) && (GameData.playerLevel[i] > GameData.playerFinalLevel))
        {
            print("Skip Player:" + i);
            i++;
        }
        if (i < 10)  // confirm score is high enough to be included in list (must be better than/as good as at least one other)
        {
            // insert score
            print("Save player at level:" + i);
            savedLevel = GameData.playerLevel[i];
            savedInitials = GameData.playerNames[i];
            GameData.playerLevel[i] = GameData.playerFinalLevel;
            GameData.playerNames[i] = GameData.playerInitials;

            // push remaining scores down the list, dropping the last one
            for (int j = i + 1; j < 10; j++)
            {
                print("push other players down");
                savedLevel1 = GameData.playerLevel[j];
                savedInitials1 = GameData.playerNames[j];
                GameData.playerLevel[j] = savedLevel;
                GameData.playerNames[j] = savedInitials;
                savedLevel = savedLevel1;
                savedInitials = savedInitials1;
            }
        }
        GameData.addPlayer = false;
        writeTopScoresToFile();
    }

    // Check for user input requesting to launch or quit game
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))  // Check to launch Play for Score Mode
        {
            int sceneCounter = SceneManager.GetActiveScene().buildIndex + 1;
            GameData.flightSchool = false;
            SceneManager.LoadScene(sceneCounter);
        }
        if (Input.GetKeyDown(KeyCode.F))  // Check to launch Flight School Mode
        {
            GameData.flightSchool = true;
            SceneManager.LoadScene(GameData.getInitialsLevelNum+1);
        }
        if (Input.GetKeyDown(KeyCode.Q))  // Check to quit game
        {
            writeTopScoresToFile();
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.Delete))  // Reset top score list
        {
            print("Reset Scores");
            for (int i=0; i<10; i++ )
            {
                GameData.playerLevel[i] = 0;
                GameData.playerNames[i] = "---";
                updateUITopScoreList();
            }
        }
    }

    private static void writeTopScoresToFile()
    {
        string path = @"c:\Temp\TopScores.txt";
        if (!File.Exists(path))
        {
            // create file
        }

        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < 10; i++)
        {
            sb.AppendLine(GameData.playerLevel[i] + " " + GameData.playerNames[i]);
        }
        File.WriteAllText(path, sb.ToString());
    }
}
