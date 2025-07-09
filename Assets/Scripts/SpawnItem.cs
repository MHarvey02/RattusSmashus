using UnityEngine.Events;
using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    public UnityEvent givePlayerAbility;
    [SerializeField]
    private PlayerContext _myPlayer;
    public void Start()
    {
        gameObject.SetActive(false);
        givePlayerAbility.AddListener(_myPlayer.GiveDoubleJump);
    }

    public void Spawn()
    {
        gameObject.SetActive(true);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            givePlayerAbility.Invoke();
            gameObject.SetActive(false);
        }
    }
}
