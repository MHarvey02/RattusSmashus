using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerBaseState
{
    public override void EnterState(PlayerContext player)
    {
        player.myAnimator.SetTrigger("isWallJumping");
        player.movementComp.WallJump();
        ExitState(player, player.InAirState, null);
    }

    public override void ExitState(PlayerContext player, PlayerBaseState nextState, bool? isMovingHorizontal)
    {
        player.myAnimator.SetTrigger("isWallJumping");
        player.SetState(player.InAirState,null);
    }
    

}
