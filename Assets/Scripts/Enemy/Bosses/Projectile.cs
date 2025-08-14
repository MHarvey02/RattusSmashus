using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

    public class Projectile : MonoBehaviour
    {

        private Vector2 _direction;
        [SerializeField]
        float speed = 3f;

        [SerializeField]
        private Rigidbody2D _myRB;

        private IEnumerator _myCoroutine;

        [SerializeField]
        private TrailRenderer _myTrail;

    public void Awake()
    {
        _myTrail = GetComponent<TrailRenderer>();
    }
    void OnEnable()
    {
        _myCoroutine = TimeAlive();
        StartCoroutine(_myCoroutine);
        _myTrail.Clear();
    }
            
    //Set initial location of the projectile        
    public void SetLocation(Vector3 endTransform)
    {
        _direction = endTransform - transform.position;
        //Adding random spread to the shot
        _direction += new Vector2(0, Random.Range(-3, 3));
        _myRB.linearVelocity = _direction * speed;
    }

    public void PlayerShoot(Vector3 endTransform)
    {
        _direction = endTransform - transform.position;
        Debug.Log(_direction);
        //Adding random spread to the shot

        _myRB.linearVelocity = _direction * speed;
    }

    public void SetVelocity(Vector2 speed)
    {
        _myRB.linearVelocity = speed;
    }

    //How long the projectile should be active for
    private IEnumerator TimeAlive()
    {
        yield return new WaitForSecondsRealtime(3);
        _myCoroutine = TimeAlive();
        gameObject.SetActive(false);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "trap")
        { return; }
        gameObject.SetActive(false);
        StopCoroutine(_myCoroutine);
    }
    
}


