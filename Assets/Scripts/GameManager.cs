using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using NUnit.Framework.Internal;
using System;

//Keeps track of what levels to load and handles reseting a level upon death
public class GameManager : MonoBehaviour
{

    void Awake()
    {
        //Make sure there is only one game manager
        int numGameSessions = FindObjectsByType<GameManager>(FindObjectsSortMode.None).Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

    }
    //Static so it can be called by the player events
    static public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    static public void LoadNextLevel()
    {
        LevelResultsScreen.nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings - 1);
    }

    static public void DrawDeadText()
    {
        return;
    }

}
