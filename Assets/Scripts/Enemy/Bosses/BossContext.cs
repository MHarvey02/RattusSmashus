using Enemy.Boss.States;
using UnityEngine;


namespace Enemy.Boss
{   
    public class BossContext : MonoBehaviour
    {
        Base _currentState;
        #region 
        public Move move = new();
        public Chase chase = new();
        public Dead dead = new();
        #endregion
        [SerializeField]
        public Vision myVision;

        [SerializeField]
        public GameObject myPlayer;

        [SerializeField]
        public Projectile projectile;

        [SerializeField]
        public float moveSpeed = 5;

        [SerializeField]
        public Transform deathPosition;

        [SerializeField]
        public BossSounds MySounds;


        public void Awake()
        {
            myVision = GetComponent<Vision>();
        }
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {   
            SetState(move);
        }

        public void SetState(Base nextState)
        {
            _currentState = nextState;
            _currentState.EnterState(this);
        }
        //Fires a projectile towards the player
        public void Attack()
        {
            Projectile bullet = EnemyObjectPool.SharedInstance.GetPooledObject();
            MySounds.Shoot();
            if (bullet != null)
            {
                bullet.transform.position = transform.position;
                bullet.transform.rotation = transform.rotation;
                bullet.gameObject.SetActive(true);
                bullet.SetLocation(myPlayer.transform.position);

            }
        }

        public void Death()
        {
            SetState(dead);
        }

        public void Despawn()
        {
            gameObject.SetActive(false);
        }

        public void OnTriggerExit2D(Collider2D collision) => _currentState.OnTriggerExit2D(collision, this);
        // Update is called once per frame
        public void Update() => _currentState.Update(this);

    }
   

}
