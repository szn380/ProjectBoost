using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using System.Runtime.Serialization.Formatters;
using UnityEngine.UI;

public class Restart : MonoBehaviour
{
    public InputField iField;
    string playerInitials;

    private void Start()
    {
        iField.onEndEdit.AddListener(delegate { inputIField(iField); });
    }

    public void inputIField(InputField userInput)
    {
        print("Input Field " + userInput.text);
        GameData.addPlayer = true;
        GameData.playerInitials = userInput.text;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(1);
        }
        else if (Input.GetKeyDown(KeyCode.Q)) {
            SceneManager.LoadScene(0);
        }
    }
}
