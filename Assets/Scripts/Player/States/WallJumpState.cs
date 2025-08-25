using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WallJumpState : BaseState
{
    private bool _isMoving = true;
    public WallJumpState(bool isMoving = false)
    {
        _isMoving = isMoving;
    }

    public override void Move(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        if (inputContext.started)
        {
            _isMoving = true;
        }

        if (inputContext.canceled)
            {
                _isMoving = false;
            }
        player.myMovementComp.SetDirection(inputContext.ReadValue<Vector2>().x);      
    }
    

    public override void EnterState(PlayerContext player)
    {
        player.myAnimator.SetTrigger("isWallJumping");
    }

    public override void FixedUpdate(PlayerContext player)
    {
        if (!player.myCollision.IsOnStillWall() && !player.myCollision.IsTouchingWall())
        {
            player.SetState(new InAirState(_isMoving));
        } 
    }
}
