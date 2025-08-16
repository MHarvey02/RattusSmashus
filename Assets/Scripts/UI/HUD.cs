using System.Collections;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{

    [SerializeField]
    private TMP_Text _deathText;

    [SerializeField]
    private SpriteRenderer _myShotgunSprite;

    [SerializeField]
    private IEnumerator _alphaResetCoroutine;

    [SerializeField]
    public float _alphaResetTime;

    [SerializeField]
    private Color _startColour;

    [SerializeField]
    private TMP_Text _timerText;

    [SerializeField]
    private float _timerValue;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _deathText.text = TestToolStatic.death.ToString();
        _alphaResetCoroutine = RaiseShotgunAlpha();
        _startColour = _myShotgunSprite.color;
    }
    //Shows the player the shotgun is on cooldown as it is grayed out
    public void LowerShotgunAlpha()
    {
        _myShotgunSprite.color -= new Color(0, 0, 0, 0.5f);

        StartCoroutine(_alphaResetCoroutine);
    }
    //Reset the shotgun HUD element to show it can be used again
    public IEnumerator RaiseShotgunAlpha()
    {
        yield return new WaitForSecondsRealtime(_alphaResetTime);

        _myShotgunSprite.color = _startColour;
        _alphaResetCoroutine = RaiseShotgunAlpha();
    }
    //Submit the level time so it can be displayed on the results screen
    public void LevelEnd()
    {
        LevelResultsScreen.mostRecentTime = _timerValue;
    }

    // Update is called once per frame
    void Update()
    {
        //Track how long the player has been on this level attempts (resets on death)
        _timerValue += Time.deltaTime;
        //Drawing the time t0 the HUD element
        _timerText.text = _timerValue.ToString("0.00");
    }
}
