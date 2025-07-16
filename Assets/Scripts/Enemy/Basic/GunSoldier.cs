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


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _myCoroutine = Shoot();
            StartCoroutine(_myCoroutine);
            
        }

        private IEnumerator Shoot()
        {
            
            yield return new WaitForSecondsRealtime(_timeBetweenShots);
            //spawn bullet
            Projectile bullet = ObjectPool.SharedInstance.GetPooledObject();
            if (bullet != null)
            {
                Debug.Log("fire");
                
                bullet.transform.position = transform.position;
                bullet.gameObject.SetActive(true);
                bullet.SetVelocity(_direction);
                
            }
            
                _myCoroutine = Shoot();
                StartCoroutine(_myCoroutine);
        }



        // Update is called once per frame
        void Update()
        {

        }
}
