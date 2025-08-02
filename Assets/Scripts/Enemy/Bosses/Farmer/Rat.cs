using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Rat : MonoBehaviour
{
    [SerializeField]
    private Vector2 _speed = new(10, 0);
    [SerializeField]
    private Rigidbody2D _myRB;

    [SerializeField]
    private Animator _animator;

    [SerializeField]
    public FarmerOnRat _farmer;

    [SerializeField]
    private GameObject _deathNail;

    [SerializeField]
    private SpawnItem _shotgunItem;
    [SerializeField]
    private ParticleSystem _myParticleSystem;

    [SerializeField]
    private List<BoxCollider2D> _myColliders;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _myRB.linearVelocity = _speed;
        _animator.Play("Idle");
    }

    void Death()
    {
        _myParticleSystem.Stop();
        _animator.Play("RatDeath");
        foreach (BoxCollider2D collider in _myColliders){
            collider.enabled= false;
        }

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Nail")
        {
            Death();
        }
        if (collision.gameObject.tag == "BossKill")
        {
            _shotgunItem.Spawn();
            _myRB.linearVelocity = new(0, 0);
            _farmer.FallOffRat();
            _deathNail.SetActive(true);
        } 
    }

}
