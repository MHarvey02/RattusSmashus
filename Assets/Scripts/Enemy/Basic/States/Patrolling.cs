using System.Collections;
using UnityEngine;

//This is currently unused
namespace Enemy.Melee.States
{
    public class Patrolling : Base
    {
        public override void EnterState(StateContext enemy)
        {
            enemy.StartCoroutine(Attack(enemy));

        }

        private IEnumerator Attack(StateContext enemy)
        {
            yield return new WaitForSecondsRealtime(enemy.TimeBetweenAttacks);
            enemy.SetState(enemy.Attacking);
        }

        public override void Update(StateContext enemy)
        {
            enemy.Path.Move();
        }

    }

}