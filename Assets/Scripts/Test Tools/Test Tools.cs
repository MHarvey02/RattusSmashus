using System;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.SceneManagement;

//This class tracks the players time and deaths across the whole game.
//It is purely for testing purposes
public class TestTools : MonoBehaviour
{

    public static TestTools Instance;

    [SerializeField]
    private static float _time { get; set; }

    public void Awake()
    {
        // start of new code
        if (Instance != null)
        {

            Destroy(gameObject);
            return;
        }
        // end of new code
        TestToolStatic.death = 0;
        _time = 0;
        Instance = this;
        DontDestroyOnLoad(gameObject);

    }

    public static void CountDeath()
    {
        TestToolStatic.death++;
    }

    public static void OnLevelEnd()
    {
        TestToolStatic.timeList.Add(_time);
        TestToolStatic.deathList.Add(TestToolStatic.death);
        TestToolStatic.death = 0;
        _time = 0;
        TestToolStatic.LevelList.Add(SceneManager.GetActiveScene().name);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _time += Time.deltaTime;
    }
}
