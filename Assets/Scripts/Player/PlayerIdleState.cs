using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerIdleState : PlayerBaseState
{
    public override void EnterState(PlayerContext player)
    {
        Debug.Log(this);

        player.movementComp.resetMaxMoveSpeed();
    }
    public override void Move(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        if (inputContext.started)
        {
            player.SetState(player.MoveState);    
        }
        
    }

    public override void Jump(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        player.SetState(player.JumpState);
    }

    public override void FixedUpdate(PlayerContext player)
    {
        if (!player.movementComp.GroundCollisionCheck())
        {
            player.SetState(player.InAirState);
        } 

    }
}
