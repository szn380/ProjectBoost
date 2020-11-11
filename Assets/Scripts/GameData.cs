using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameData : MonoBehaviour
{
    // Game has two primary modes:
    //    Flight School - learn how to fly rocket. Completing levels does not count towards score.
    //                    These levels/scenes are names FSx, and come at the end of the scene list in build settings
    //    Play for Score - main mode of the game. After each level completed, the player earns a score of that level number.
    //                     When the player wins (finishes the game) or dies, they are assigned a score of the last level they completed
    //                     and they enter their initials to be displayed in the top games list on the Title level/scene
    
    // The GameData class persists across scenes so that the game data can be available 
    // for any scene, thus allowing sharing of data across scenes.
    // By making the following variables static, they are associated with the class (not class instance),
    // so they can be access by using the class (GameData.xxx)
    // This script defining the GameData class must be assigned to a GameObject in the first scene

    // top player list is made of player initials (playerNames[]) and player score (playerLevel[]) pairs. These are stored as two arrays
    public static int[] playerLevel = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public static string[] playerNames = new string[10] { "---", "---", "---", "---", "---", "---", "---", "---", "---", "---"};
    public static int playerFinalLevel = 0;         // stores the players score (the highest level they have completed
    public static string playerInitials = "---";    // stores the player initials when they win or die
    public static bool addPlayer = false;           // player has died or completed game, so their initials should be added to top player list
    public static bool flightSchool = false;        // is game in "flight school" mode (vs. scoring mode)
    public static int getInitialsLevelNum = 11;      // constant for the level number to load to get players initials
 
    // Awake is called before the first frame update
    void Awake()
    {
        // Make this game object persist across levels/scenes
        DontDestroyOnLoad(this);
        loadTopScoresFromFile();
    }
    
    void loadTopScoresFromFile()
    {
        print("Load Scores From File");
        string path = @"c:\Temp\TopScores.txt";
        if (File.Exists(path))
        {
            string[] lines = File.ReadAllLines(path);
            int i = 0;
            foreach (string line in lines)
            {
                if (i < 10)
                {
                    string[] textFields = line.Split(' ');
                    playerLevel[i] = int.Parse(textFields[0]);                    
                    playerNames[i] = textFields[1]; 
                    if (textFields.Length > 2)  // handle spaces in text line 
                    {
                        for (int j=2; j<textFields.Length; j++ )
                        {
                            playerNames[i] = playerNames[i] + " " + textFields[j];
                        }
                    }
                    i++;
                }
            }
        }
    }
}
