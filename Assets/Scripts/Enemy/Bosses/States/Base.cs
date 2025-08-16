using Unity.VisualScripting;
using UnityEngine;

namespace Enemy.Boss.States
{
    public abstract class Base
    {

        public virtual void EnterState(BossContext boss)
        {

        }
        
        public virtual void OnTriggerExit2D(Collider2D collision, BossContext boss)
        {
            if (collision.gameObject.tag == "Player")
            {
                CalculateDirection(boss, collision.gameObject);
            }
        }
        
        private void CalculateDirection(BossContext boss, GameObject player)
        {
            if (boss.transform.position.x > player.transform.position.x)
            {
                boss.myVision.direction = -1;
                return;
            }
            boss.myVision.direction = 1;
        }
        
        public virtual void Update(BossContext boss) { }

}  
}

