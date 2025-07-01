using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJumpState : BaseState
{
    public override void EnterState(PlayerContext player)
    {
        player.myAnimator.SetTrigger("isWallJumping");
        player.movementComp.WallJump();
        ExitState(player, player.InAirState, null);
    }

    public override void ExitState(PlayerContext player, BaseState nextState, bool? isMovingHorizontal)
    {
        player.myAnimator.SetTrigger("isWallJumping");
        player.SetState(player.InAirState,null);
    }
    

}
