using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveState : PlayerBaseState
{
    public override void EnterState(PlayerContext player)
    {
        player.myAnimator.SetBool("isRunning", true);
    }

    //Actions
    public override void Move(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        if (inputContext.canceled)
        {
            ExitState(player, player.IdleState, null);
        }
    }
    
    public override void Jump(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        ExitState(player,player.JumpState,true);
    }

    public override void Slide(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        if (inputContext.started)
        {
           player.SetState(player.SlideState); 
        }
        
    }

    public override void ExitState(PlayerContext player, PlayerBaseState nextState, bool? isMovingHorizontal)
    {
        player.myAnimator.SetBool("isRunning",false);
        player.SetState(nextState,isMovingHorizontal);
    }

        public override void Shoot(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        player.SetState(player.knockbackState, true);
    }


    //Update
    public override void FixedUpdate(PlayerContext player)
    {
        
        if (!player.movementComp.GroundCollisionCheck())
        {
            ExitState(player, player.InAirState, true);
        }

        if (player.movementComp.WallCollisionCheck())
        {
            player.movementComp.resetMaxMoveSpeed();
        }
        player.movementComp.HorizontalMove();
    }

}
