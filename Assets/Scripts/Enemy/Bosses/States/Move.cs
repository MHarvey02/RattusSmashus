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
            playLoc.x -= 5 * boss.myVision.direction;
            
            //Move the boss towards the player  
            boss.transform.position = Vector2.MoveTowards(boss.transform.position, playLoc, boss.moveSpeed * Time.deltaTime);
            //Check if the player is close enough to chase
            if (boss.myVision.DrawBoxCast() != null)
            {
                boss.SetState(boss.chase);
            }
    }
}

}
