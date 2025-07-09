using System.Collections;
using UnityEngine;


namespace Enemy.Boss.States
{
    
    //get this working
    public class Chase : Base
    {

        private float _moveSpeed = 5;

        private float _exitChaseTime = 3;

        public IEnumerator coroutine;

        public override void EnterState(BossContext boss)
        {
            coroutine = ExitChase(boss);
            boss.StartCoroutine(coroutine);
        }

        public override void Update(BossContext boss)
        {
            Vector2 location = boss.myPlayer.transform.position + new Vector3(0, 5, 0);
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

            }
        }

    }
}






