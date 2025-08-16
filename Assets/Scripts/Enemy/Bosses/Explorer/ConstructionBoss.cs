using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionBoss : MonoBehaviour
{
    [SerializeField]
    private List<BossTrap> _myTraps;

    [SerializeField]
    private IEnumerator _myCoroutine;

    [SerializeField]
    private float _timeBetweenAttacks = 5;

    [SerializeField]
    private BossSounds _mySounds;

    [SerializeField]
    private GameObject _exitBlocker;

    [SerializeField]
    private SpawnItem _myItem;
    [SerializeField]
    private Rigidbody2D _myRB;

    private int _health = 3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _myCoroutine = DelayAttack();
        StartCoroutine(_myCoroutine);
    }
    
    //This stops the boss attacking on the first frame
    private IEnumerator DelayAttack()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        _myCoroutine = Attack();
        StartCoroutine(_myCoroutine);
    }

    private IEnumerator Attack()
    {
        //Work out which traps to activate add to list
        _mySounds.ReadyAttack();
        foreach (BossTrap trap in _myTraps)
        {
            //There has to be a better way
            if (Random.Range(0, 2) == 1)
            {
                trap.PrepareAttack();
            }
        }
        yield return new WaitForSecondsRealtime(_timeBetweenAttacks);
        _myCoroutine = Attack();
        StartCoroutine(_myCoroutine);

    }
    //When a button is pressed by the player take damage
    public void TakeDamage()
    {
        _health--;
        if (_health <= 0)
        {
            Death();
        }
    }

    //Despawn the traps, open the arena exit and spawn the item
    private void Death()
    {
        StopCoroutine(_myCoroutine);
        foreach (BossTrap trap in _myTraps)
        {
            trap.Despawn();
        }
        _exitBlocker.SetActive(false);
        _myItem.Spawn();
        _myRB.simulated = true;

    }

}
