using System.Collections.Generic;
using TMPro;
using UnityEngine;

//This display the results of the players playthrough in one place
//It is only important in testing
public class DisplayResults : MonoBehaviour
{


    [SerializeField]
    List<TMP_Text> deathTexts;

    [SerializeField]
    List<TMP_Text> timeTexts;

    [SerializeField]
    List<TMP_Text> levelNames;

    void Start()
    {
        DrawText();
    }

    void DrawText()
    {
        Debug.Log(TestToolStatic.deathList.Count);

        for (int i = 0; i < TestToolStatic.deathList.Count; i++)
        {

            deathTexts[i].text = TestToolStatic.deathList[i].ToString();
            timeTexts[i].text = TestToolStatic.timeList[i].ToString();
            levelNames[i].text = TestToolStatic.LevelList[i].ToString();
            deathTexts[i].enabled = true;
            timeTexts[i].enabled = true;
            levelNames[i].enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
