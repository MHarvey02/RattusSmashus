using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJumpState : BaseState
{


    public override void EnterState(PlayerContext player)
    {
        player.myAnimator.SetTrigger("isWallJumping");
        player.myMovementComp.WallJump();
        player.myAnimator.SetTrigger("isWallJumping");
        player.SetState(new InAirState());
    }

}
