using System.Collections;
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

    private IEnumerator _myCoroutine;


    void OnEnable(){
        _myCoroutine = TimeAlive();
        StartCoroutine(_myCoroutine);}

    public void SetLocation(Vector3 endTransform)
    {
        _direction = endTransform - transform.position;
    }

    private IEnumerator TimeAlive()
    {
        yield return new WaitForSecondsRealtime(3);
        _myCoroutine = TimeAlive();
        Debug.Log("yes");
        gameObject.SetActive(false);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.SetActive(false);
        StopCoroutine(_myCoroutine);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        _myRB.linearVelocity = _direction * speed;

    }
}
