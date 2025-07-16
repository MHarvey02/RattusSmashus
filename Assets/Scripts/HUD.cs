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

    public void LowerShotgunAlpha()
    {
        _myShotgunSprite.color -= new Color(0, 0, 0, 0.5f);

        StartCoroutine(_alphaResetCoroutine);
    }

    public IEnumerator RaiseShotgunAlpha()
    {
        yield return new WaitForSecondsRealtime(_alphaResetTime);

        _myShotgunSprite.color = _startColour;
        _alphaResetCoroutine = RaiseShotgunAlpha();
    }

    // Update is called once per frame
    void Update()
    {
        _timerValue += Time.deltaTime;
        _timerText.text = _timerValue.ToString("0.00");
    }
}
