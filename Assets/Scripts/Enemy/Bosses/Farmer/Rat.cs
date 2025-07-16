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

    private UnityEvent _deathEvent;

    [SerializeField]
    private GameObject _deathNail;

    [SerializeField]
    private GameObject _shotgunItem;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        _myRB.linearVelocity = _speed;
        _animator.Play("Idle");
    }

    void Death()
    {
        _animator.Play("RatDeath");
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Nail")
        {
            Death();
        }
        if (collision.gameObject.tag == "BossKill")
        {
            _shotgunItem.SetActive(true);
            _myRB.linearVelocity = new(0, 0);
            _farmer.FallOffRat();
            _deathNail.SetActive(true);

        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
