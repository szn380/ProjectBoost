using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using System.Runtime.Serialization.Formatters;
using UnityEngine.UI;
using TMPro;

public class Restart : MonoBehaviour
{
    public TMP_InputField iField;
    string playerInitials;

    private void Start()
    {
        if (iField != null)
        {
            iField.text = "Enter Initials";
        }
        else
        {
            print("ERROR with TMP iField: is null on Start()");
        }
    }

    public void processTextBox()
    {
        if (iField != null)
        {
            print("Input Field " + iField.text);
            GameData.addPlayer = true;
            GameData.playerInitials = iField.text;
        }
        else
        {
            print("ERROR with TMP iField: is null on processTextBox()");
        }

    }

    public void inputIField(TMP_InputField userInput)  // TBD delme
    {
        // print("Input Field " + userInput.text);
        //GameData.addPlayer = true;
        //GameData.playerInitials = userInput.text;
    }

    void Update()  // TBD delme
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // SceneManager.LoadScene(1);
        }
    }

    public void processMainMenuButton()
    {
        SceneManager.LoadScene(0);
    }
}
