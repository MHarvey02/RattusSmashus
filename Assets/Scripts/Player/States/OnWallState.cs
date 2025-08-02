using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class OnWallState : BaseState
{
    private bool _isMoving = false;

    public OnWallState(bool isMoving = false)
    {
        _isMoving = isMoving;
    }
    public override void EnterState(PlayerContext player)
    {
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
            player.myCollision.ChangeDirection();
            player.SetState(new WallJumpState());
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
        //there is currently an issue where the play will move in their last direction when exiting the state this way
        if (!player.myCollision.IsTouchingWall())
        {
            player.SetState(new InAirState(_isMoving));
        }
        player.myMovementComp.HorizontalMoveInAir();
    }
}
