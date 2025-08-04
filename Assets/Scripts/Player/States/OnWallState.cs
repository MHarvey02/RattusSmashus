using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class OnWallState : BaseState
{
    private bool _isMoving = false;
    private float _jumpDirection;
    public OnWallState(bool isMoving = false)
    {
        _isMoving = isMoving;
    }
    public override void EnterState(PlayerContext player)
    {
        _jumpDirection = player.myMovementComp.wallJumpDirection;
        if (player.myMovementComp.wallJumpDirection == player.myMovementComp.Direction)
        {
            _jumpDirection *= -1;
        }
        
        player.myAnimator.Play("WallSlide");
        player.myMovementComp.HitWall();
    }

    public override void Move(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        player.myMovementComp.SetDirection(inputContext.ReadValue<Vector2>().x);
        if (inputContext.started)
        {
            _isMoving = true;
        }
        if (inputContext.canceled)
        {
            _isMoving = false;
        }
    }
    
    public override void Jump(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        if (inputContext.canceled)
        {
            return;
        }
        if (inputContext.started)
        {
            player.mySounds.Jump();
            player.myMovementComp.WallJump(_jumpDirection);
            player.myCollision.ChangeDirection(_jumpDirection); 
            player.SetState(new WallJumpState(_isMoving));
        }

    }

    public override void Grapple(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        return;
    }

    public override void FixedUpdate(PlayerContext player)
    {

        //Change to variable
        player.myMovementComp.currentMoveSpeedCap += 0.5f * Time.deltaTime;

        
        if (player.myCollision.IsTouchingGround())
        {
            player.SetState(new IdleState());
        }
        //There are two checks to give the player some leniency on being able to wall jump while moving away from the wall
        if (!player.myCollision.IsTouchingWall() && !player.myCollision.IsOnStillWall())
        {
            player.SetState(new InAirState(_isMoving));
        }
        player.myMovementComp.HorizontalMoveInAir();
    }
}
