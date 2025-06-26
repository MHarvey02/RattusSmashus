using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveState : PlayerBaseState
{


    //Actions
    public override void Move(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        if (inputContext.canceled)
        {
            player.SetState(player.IdleState);
        }
    }
    
    public override void Jump(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        player.SetState(player.JumpState,true);
    }

    public override void Slide(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        if (inputContext.started)
        {
           player.SetState(player.SlideState); 
        }
        
    }

    //Update
    public override void FixedUpdate(PlayerContext player)
    {
        player.movementComp.HorizontalMove();
        if (!player.movementComp.GroundCollisionCheck())
        {
            player.SetState(player.InAirState, true);
        }

        if (player.movementComp.WallCollisionCheck())
        {
            player.movementComp.resetMaxMoveSpeed();
        }
    }

}
