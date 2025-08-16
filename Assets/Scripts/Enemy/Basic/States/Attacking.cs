using System.Collections;
using UnityEngine;

//This is currently unused
namespace Enemy.Melee.States
{
    public class Attacking : Base
    {
        public override void EnterState(StateContext enemy)
        {
     
            //Move back to patrol
            enemy.StartCoroutine(waitTIme(enemy));

        }

        private IEnumerator waitTIme(StateContext enemy)
        {
            yield return new WaitForSecondsRealtime(1);
            enemy.SetState(enemy.Patrolling);
        }

    }
}