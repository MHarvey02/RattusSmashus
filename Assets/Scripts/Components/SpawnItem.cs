using UnityEngine.Events;
using UnityEngine;

//Spawns the item to be given at the end of the boss level
public class SpawnItem : MonoBehaviour
{

    [SerializeField]
    private SpriteRenderer _mySprite;
    public void Start()
    {
        gameObject.SetActive(false);
    }

    public void Spawn()
    {
        gameObject.SetActive(true);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _mySprite.enabled = false;
        }
    }


}
