using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InAirState : BaseState
{
    public BaseState nextState;
    bool? isMovingHorizontal = false;

    public override void EnterState(PlayerContext player)
    {
        player.myAnimator.Play("Falling");
        
        isMovingHorizontal = false;
        nextState = player.IdleState;
    }

    public override void EnterState(PlayerContext player, bool? _isMovingHorizontal)
    {
        player.myAnimator.Play("Falling");

        isMovingHorizontal = _isMovingHorizontal;
        if (isMovingHorizontal == true)
        {
            nextState = player.MoveState;
        }
        else
        {
            nextState = player.IdleState;
        }
    }

    public override void Move(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        if (inputContext.canceled)
        {
            isMovingHorizontal = false;
            nextState = player.IdleState;
            return;
        }
        if (inputContext.started)
        {
            isMovingHorizontal = true;
            nextState = player.MoveState;
        }
            
    }

    public override void Jump(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        if (inputContext.started)
        {
            if (player.movementComp.canDoubleJump)
            {
                player.movementComp.Jump();
                player.movementComp.canDoubleJump = false;
            } 
        }
    }

    public override void Shoot(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        player.SetState(player.knockbackState, null);
    }


    public override void ExitState(PlayerContext player, BaseState nextState, bool? isMovingHorizontal)
    {
        player.movementComp.canDoubleJump = player.movementComp.hasDoubleJumpAbility;
        player.myAnimator.SetBool("isFalling", false);
        player.SetState(nextState, isMovingHorizontal);
    }

    //Updates
    public override void FixedUpdate(PlayerContext player)
    {
        player.movementComp.CheckMoveSpeed();
        if (isMovingHorizontal == true)
        {
            player.movementComp.HorizontalMove();
        }

        if (player.movementComp.GroundCollisionCheck())
        {

            ExitState(player, nextState, null);
        }

        if (player.movementComp.WallCollisionCheck())
        {
            ExitState(player, player.OnWallState, null);
        }
    }
}
