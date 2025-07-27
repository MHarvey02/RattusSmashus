using System;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestTools : MonoBehaviour
{
    //Need to be persistent across whole game.
    //needs to count deaths per level
    //needs to count time per level
    //Display results on final Scene
    // Start is called once before the first execution of Update after the MonoBehaviour is created


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
