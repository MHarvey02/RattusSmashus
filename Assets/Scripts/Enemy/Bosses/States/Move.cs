using UnityEngine;

namespace Enemy.Boss.States
{
    public class Move : Base
{
        public override void EnterState(BossContext boss)
        {
        }
        // Update is called once per frame
        public override void Update(BossContext boss)
        {

            Vector2 playLoc = boss.myPlayer.transform.position;
            playLoc.y += 3;
            
            
            boss.transform.position = Vector2.MoveTowards(boss.transform.position, playLoc, boss.moveSpeed * Time.deltaTime);

            if (boss.myVision.DrawBoxCast() != null)
            {
                boss.SetState(boss.chase);
            }
    }
}

}
