using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

public class FarmerOnRat : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField]
    private PlayerContext _myPlayer;

    [SerializeField]
    private Rigidbody2D _myRB;

    [SerializeField]
    private float _shotTime;

    [SerializeField]
    public Projectile projectile;

    
    private IEnumerator _shootCoroutine;
    private bool isSpinning = false;

    private Vector2 _playerDirection;
    [SerializeField]
    private float _bulletSpeed;

    [SerializeField]
    private int _bulletsPerShot;

    [SerializeField]
    private BossSounds _mySounds;

    void Start()
    {
        _shootCoroutine = Shoot();
        StartCoroutine(_shootCoroutine);
    }
    //Death of the rat leads to the farmer flying off of it
    public void FallOffRat()
    {

        _myRB.bodyType = RigidbodyType2D.Dynamic;
        _myRB.AddForce(new(1000, 50));
        isSpinning = true;
        StopCoroutine(_shootCoroutine);
        StartCoroutine("Despawn");
    }
    //Time between shots from the farmer
    // //Spawn projectile from pool, give random spread, move in direction of player
    private IEnumerator Shoot()
    {
        _mySounds.ReadyAttack();
        yield return new WaitForSecondsRealtime(_shotTime);
        

        _shootCoroutine = Shoot();
        StartCoroutine(_shootCoroutine);
        //Spawn bullet
        _mySounds.Shoot();
        _playerDirection = _myPlayer.transform.position - transform.position;
        for (int i = 0; i < _bulletsPerShot; i++)
        {
            Projectile bullet = EnemyObjectPool.SharedInstance.GetPooledObject();
            if (bullet != null)
            {

                bullet.transform.position = transform.position;
                _playerDirection += new Vector2(0, Random.Range(0, 5));

                _playerDirection *= _bulletSpeed;
                _playerDirection = Vector2.ClampMagnitude(_playerDirection, 20);
                bullet.gameObject.SetActive(true);
                bullet.SetVelocity(_playerDirection);

            }
            _mySounds.Shoot();
        }   
    }
    //Despawn the boss after he has flown off of the rat
    IEnumerator Despawn()
    {
        yield return new WaitForSecondsRealtime(5);
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isSpinning)
        {
            transform.Rotate(0,0,10);
        }
    }
}
