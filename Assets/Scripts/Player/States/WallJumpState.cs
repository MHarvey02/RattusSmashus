using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJumpState : BaseState
{
    private bool _isMoving = true;
    public WallJumpState(bool isMoving = false)
    {
        _isMoving = isMoving;
    }

    public override void EnterState(PlayerContext player)
    {
        player.myAnimator.SetTrigger("isWallJumping");
        player.myAnimator.SetTrigger("isWallJumping");
        Debug.Log("wall jmuped");
        player.SetState(new InAirState(_isMoving));
    }

}
