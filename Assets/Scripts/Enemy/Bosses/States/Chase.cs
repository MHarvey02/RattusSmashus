using System.Collections;
using UnityEngine;


namespace Enemy.Boss.States
{
    
    //get this working
    public class Chase : Base
    {

        private float _moveSpeed = 10f;

        private float _exitChaseTime = 3.5f;

        private  Vector3 _distanceFromPlayer;

        public IEnumerator coroutine;

        public override void EnterState(BossContext boss)
        {
            _distanceFromPlayer = new(5 * boss.myVision.direction, 3, 0);
            coroutine = ExitChase(boss);
            boss.StartCoroutine(coroutine);
        }

        public override void Update(BossContext boss)
        {
            Vector2 location = boss.myPlayer.transform.position + _distanceFromPlayer;
            boss.transform.position = Vector2.MoveTowards(boss.transform.position, location, _moveSpeed * Time.deltaTime);
           
        }
    
        public IEnumerator ExitChase(BossContext boss)
        {
            boss.MySounds.ReadyAttack();
            yield return new WaitForSecondsRealtime(_exitChaseTime);
            if (boss.myVision.DrawBoxCast() == null)
            {
                boss.SetState(boss.move);
            }
            else
            {
                boss.Attack();
                _moveSpeed += 1;
                coroutine = ExitChase(boss);
                boss.StartCoroutine(coroutine);
                _distanceFromPlayer.y -= 1.5f;
                _distanceFromPlayer.x += 1.5f * boss.myVision.direction;

                if (_distanceFromPlayer.y < 0)
                {
                    _distanceFromPlayer.y = 0;
                    _distanceFromPlayer.x = 0;
                }
                 
                

            }
        }

    }
}






