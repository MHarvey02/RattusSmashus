using System.Collections;
using UnityEngine;

public class GunSoldier : MonoBehaviour
{

        [SerializeField]
        private float _timeBetweenShots = 1.0f;

        [SerializeField]
        private IEnumerator _myCoroutine;

        [SerializeField]
        private Vector2 _direction = new(1,0);

        [SerializeField]
        public Projectile projectile;
        
        [SerializeField]
        private BossSounds _mySounds;


        // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _myCoroutine = Shoot();
        StartCoroutine(_myCoroutine); 
    }
    
    //Time between shots from the emeny
    private IEnumerator Shoot()
    {
        yield return new WaitForSecondsRealtime(_timeBetweenShots);
        //spawn bullet
        Projectile bullet = EnemyObjectPool.SharedInstance.GetPooledObject();
        if (bullet != null)
        {

            bullet.transform.position = transform.position;
            _mySounds.Shoot();
            bullet.gameObject.SetActive(true);
            bullet.SetVelocity(_direction);

        }

        _myCoroutine = Shoot();
        StartCoroutine(_myCoroutine);
    }

}
