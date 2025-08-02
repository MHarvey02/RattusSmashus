using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GiveShotgun : MonoBehaviour
{

    [SerializeField]
    public PlayerContext myPlayer;

    [SerializeField]
    private TMP_Text _toolTip;

    [SerializeField]
    private SpriteRenderer _mySprite;

    public UnityEvent _givePlayerItem;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void OnEnable()
    {
        _mySprite.enabled = true;
        _givePlayerItem.AddListener(myPlayer.GiveShotgun);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _givePlayerItem.Invoke();
            _toolTip.enabled = true;
            _mySprite.enabled = false;

        }
    }
}
