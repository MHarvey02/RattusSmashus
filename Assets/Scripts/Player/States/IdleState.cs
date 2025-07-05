using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;

public class IdleState : BaseState
{
    public override void EnterState(PlayerContext player)
    {
        player.movementComp.resetMaxMoveSpeed();
        player.movementComp.rb.linearVelocity = new Vector2(0,0);
        player.myAnimator.Play("Idle");
        
    }

    public override void Move(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        if (inputContext.started)
        {
            ExitState(player, player.MoveState, null);
        }

    }
    public override void Jump(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        ExitState(player, player.JumpState, null);
    }

    public override void Shoot(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        ExitState(player, player.knockbackState, null);
    }

    public override void Grapple(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        return;
    }

    public override void ExitState(PlayerContext player, BaseState nextState, bool? isMovingHorizontal)
    {
        player.SetState(nextState, isMovingHorizontal);
    }

    public override void FixedUpdate(PlayerContext player)
    {
        if (!player.movementComp.GroundCollisionCheck())
        {
            ExitState(player, player.InAirState, null);
        }

    }
}
