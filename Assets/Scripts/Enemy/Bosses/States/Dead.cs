using UnityEngine;


namespace Enemy.Boss.States
{
    public class Dead : Base
    {
        public override void Update(BossContext boss)
        {
            boss.transform.position = Vector2.MoveTowards(boss.transform.position, boss.deathPosition.transform.position, 10 * Time.deltaTime);
            boss.StopAllCoroutines();
        }
    }
}

