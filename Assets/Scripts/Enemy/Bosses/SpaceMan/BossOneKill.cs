using System.Collections;
using Enemy.Boss;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

//Scripted death of the first boss
public class BossOneKill : MonoBehaviour
{

    [SerializeField]
    private BossContext _myBoss;

    [SerializeField]
    private Transform _dropLocation;

    [SerializeField]
    private AudioSource _myAudioSource;

    [SerializeField]
    private AudioClip _mySound;

    [SerializeField]
    private SpawnItem _myItem;

    public UnityEvent playerEnter;
    public UnityEvent spawnItem;

    private bool _isFalling = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerEnter.AddListener(_myBoss.Death);

        spawnItem.AddListener(_myItem.Spawn);
        spawnItem.AddListener(_myBoss.Despawn);
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            playerEnter.Invoke();
        }

        if (collision.gameObject.tag == "Boss")
        {
            Fall();
            StartCoroutine("SpawnBoots");
        }

    }

    //Drop the toilet on the boss
    private IEnumerator SpawnBoots()
    {
        yield return new WaitForSeconds(1);
        _myAudioSource.PlayOneShot(_mySound);
        spawnItem.Invoke();
    }

    private void Fall()
    {
        _isFalling = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isFalling) { transform.position = Vector2.MoveTowards(transform.position, _dropLocation.position, 20 * Time.deltaTime); }
    }
}
