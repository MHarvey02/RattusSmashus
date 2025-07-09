using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    private Vector2 _direction;
    [SerializeField]
    float speed = 10f;

    [SerializeField]
    private Rigidbody2D _myRB;


    public void Awake()
    {

    }

    public void SetLocation(Transform endTransform){

        _direction = endTransform.position;

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.SetActive(false);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        _myRB.AddForce(_direction * speed );
    }
}
