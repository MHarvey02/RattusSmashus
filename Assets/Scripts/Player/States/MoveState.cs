using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveState : BaseState
{
    public override void EnterState(PlayerContext player)
    {
        player.myAnimator.Play("Running");
    }

    //Actions
    public override void Move(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        if (inputContext.canceled)
        {
            //this should change
            player.movementComp.rb.linearVelocityX = 5 * player.movementComp.direction;
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
           ExitState(player, player.SlideState, null); 
        }
        
    }

    public override void Grapple(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        return;
    }

    public override void ExitState(PlayerContext player, BaseState nextState, bool? isMovingHorizontal)
    {
        player.myAnimator.SetBool("isRunning", false);
        player.SetState(nextState, isMovingHorizontal);

    }

        public override void Shoot(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        ExitState(player, player.knockbackState, true);
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
        player.movementComp.CheckMoveSpeed();
    }

}
