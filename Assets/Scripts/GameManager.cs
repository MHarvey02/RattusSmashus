using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    Canvas mainCanvas;
    [SerializeField]
    public TMP_Text respawnText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        int numGameSessions = FindObjectsByType<GameManager>(FindObjectsSortMode.None).Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
        respawnText.enabled = false;
    }

    static public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    static public void LoadNextLevel()
    {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;

        currentLevelIndex++;

        //Making sure a next level exists
        if (currentLevelIndex > SceneManager.sceneCount)
        {
            currentLevelIndex = 0;
        }
        SceneManager.LoadScene(currentLevelIndex);
    }

    static public void DrawDeadText()
    {
        FindAnyObjectByType<GameManager>().respawnText.enabled = true;
    }

}
