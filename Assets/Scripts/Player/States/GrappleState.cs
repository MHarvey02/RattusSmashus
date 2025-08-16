using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrappleState : BaseState
{
    private BaseState _nextState = new MoveState();

    private bool _isMovingHorizontal = true;

    public override void EnterState(PlayerContext player)
    {
        player.myMovementComp.canDoubleJump = player.myMovementComp.hasDoubleJumpAbility;
        player.mySounds.Grapple();
    }
    
    public override void Move(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        player.myMovementComp.SetDirection(inputContext.ReadValue<Vector2>().x);

        if (inputContext.canceled)
        {
            _nextState =  new IdleState();
            _isMovingHorizontal = false;
        }
        else
        {
            _nextState = new MoveState();
            _isMovingHorizontal = true;
        }
        
    }
    //Let go of the grapple without jumping
    public override void Grapple(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        if (inputContext.started)
        {
           player.SetState(new InAirState(_isMovingHorizontal)); 
        }
    }

    public override void GrapplePull(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        return;
    }

    public override void Jump(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        if (inputContext.started)
        {
            player.mySounds.Jump();
            player.myMovementComp.JumpFromGrapple();
            player.SetState(new InAirState(_isMovingHorizontal)); 
        }
    }

    public override void ExitState(PlayerContext player)
    {
        player.mySounds.Grapple();
        player.myGrapple.RemoveGrappleLine();
    }
    //Updates
    public override void FixedUpdate(PlayerContext player)
    {
        player.myMovementComp.HorizontalMoveInAir();

        player.myGrapple.DrawGrappleLine();
        if (player.myCollision.IsTouchingGround())
        {
            player.SetState(_nextState);

        }

        if (player.myCollision.IsTouchingWall())
        {
            player.SetState(new OnWallState());

        }
    }
}
