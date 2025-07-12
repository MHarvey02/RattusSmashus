using System.Collections;
using UnityEngine;


namespace Enemy.Boss.States
{
    
    //get this working
    public class Chase : Base
    {

        private float _moveSpeed = 10f;

        private float _exitChaseTime = 2;

        private  Vector3 _distanceAbovePlayer;

        public IEnumerator coroutine;

        public override void EnterState(BossContext boss)
        {
            _distanceAbovePlayer = new(0, 3, 0);
            coroutine = ExitChase(boss);
            boss.StartCoroutine(coroutine);
        }

        public override void Update(BossContext boss)
        {
            Vector2 location = boss.myPlayer.transform.position + _distanceAbovePlayer;
            boss.transform.position = Vector2.MoveTowards(boss.transform.position, location, _moveSpeed * Time.deltaTime);
           
        }
    
        public IEnumerator ExitChase(BossContext boss)
        {
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
                _distanceAbovePlayer.y -= 1.5f;
                
                if (_distanceAbovePlayer.y < 0)
                {
                    _distanceAbovePlayer.y = 0;
                }
                

            }
        }

    }
}






