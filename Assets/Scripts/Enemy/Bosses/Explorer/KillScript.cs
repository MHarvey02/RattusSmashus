using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class KillScript : MonoBehaviour
{

    [SerializeField]
    private Enemy.Boss.BossContext _myBoss;

    [SerializeField]
    private List<Transform> _damageLocations;

    [SerializeField]
    private List<Trap> _traps;

    [SerializeField]
    private int _currentTrap;

    [SerializeField]
    public UnityEvent playerEnterFinalTrigger;

    public UnityEvent spawnItem;

    [SerializeField]
    private GiveGrapple _myItem;


    [SerializeField]
    private ParticleSystem _bloodEffect;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _bloodEffect.Stop();
        playerEnterFinalTrigger.AddListener(_myBoss.Death);

        spawnItem.AddListener(_myItem.Spawn);

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            MoveTrap();
        }
        if (collision.gameObject.tag == "Boss")
        {
            _bloodEffect.Play();
            spawnItem.Invoke();
        }
    }

    private void MoveTrap()
    {

        _traps[_currentTrap].BossMove(_damageLocations[_currentTrap]);
        _currentTrap++;
        if (_currentTrap == _traps.Count)
        {
            playerEnterFinalTrigger.Invoke();
            _currentTrap = 0;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
