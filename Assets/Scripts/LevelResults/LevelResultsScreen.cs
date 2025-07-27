using Mono.Cecil.Cil;
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

    [SerializeField]
    private TMP_Text _nextLevelName;

    public void Awake()
    {
        _deathCountText.text = mostRecentDeathCount.ToString();
        _mostRecentTimeText.text = mostRecentTime.ToString("0.00");
        //Debug.Log(SceneManager.GetSceneByBuildIndex(1).name);
        //_nextLevelName.text = SceneManager.GetSceneByBuildIndex(nextLevel).name;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public void LoadNextLevel()
    { 
        //Making sure a next level exists
        if (nextLevel > SceneManager.sceneCountInBuildSettings)
        {
            nextLevel = 0;
        }
        mostRecentDeathCount = 0;
        SceneManager.LoadScene(nextLevel);
    }


}
