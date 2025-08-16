using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelResultsScreen : MonoBehaviour
{

    public static float mostRecentTime { get; set; }
    public static int mostRecentDeathCount { get; set; }

    public static int nextLevel { get; set; }

    [SerializeField]
    private TMP_Text _deathCountText;

    [SerializeField]
    private TMP_Text _mostRecentTimeText;

    public void Awake()
    {
        _deathCountText.text = mostRecentDeathCount.ToString();
        _mostRecentTimeText.text = mostRecentTime.ToString("0.00");
    }


    public void LoadNextLevel()
    { 
        //Making sure a next level exists
        if (nextLevel >= SceneManager.sceneCountInBuildSettings - 1)
        {  
            nextLevel = 0;
        }
        mostRecentDeathCount = 0;
        SceneManager.LoadScene(nextLevel);
    }


}
