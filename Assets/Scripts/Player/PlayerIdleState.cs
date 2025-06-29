using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerIdleState : PlayerBaseState
{
    public override void EnterState(PlayerContext player)
    {
    
        player.movementComp.resetMaxMoveSpeed();
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
        player.SetState(player.knockbackState, null);
    }

    public override void ExitState(PlayerContext player, PlayerBaseState nextState, bool? isMovingHorizontal)
    {
        player.SetState(nextState, isMovingHorizontal);
    }

    public override void FixedUpdate(PlayerContext player)
    {
        if (!player.movementComp.GroundCollisionCheck())
        {
            player.SetState(player.InAirState);
        }

    }
}
