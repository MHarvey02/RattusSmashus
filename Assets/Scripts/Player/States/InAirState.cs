using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InAirState : BaseState
{
    private BaseState _nextState;
    private bool _isMoving;

    public override void EnterState(PlayerContext player)
    {
        player.myAnimator.Play("Falling");

    }

    public InAirState(bool isMoving = false)
    {
        _nextState = isMoving == true ? new MoveState() : new IdleState();
        _isMoving = isMoving;
    }

    public override void Move(InputAction.CallbackContext inputContext, PlayerContext player)
    {


        if (inputContext.started)
        {
            _nextState = new MoveState();
            _isMoving = true;
        }

        if (inputContext.canceled)
            {
                _nextState = new IdleState();
                _isMoving = false;
            }
        player.movementComp.SetDirection(inputContext.ReadValue<Vector2>().x);      
    }
    
    public override void Jump(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        if (inputContext.started)
        {
            if (player.movementComp.canDoubleJump)
            {
                player.mySounds.DoubleJump();
                player.movementComp.Jump();
                player.movementComp.canDoubleJump = false;
            } 
        }
    }

    public override void Shoot(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        if (inputContext.started && player.myShotgun.canShoot)
        {
          player.SetState( new KnockbackState(true));  
        }
        
    }

    //Updates
    public override void FixedUpdate(PlayerContext player)
    {
        player.movementComp.HorizontalMoveInAir();
        player.movementComp.AddtionalGravity();
        //player.movementComp.CheckMoveSpeed();


        if (player.myCollision.IsTouchingGround())
        {
            player.movementComp.canDoubleJump = player.movementComp.hasDoubleJumpAbility;


            player.SetState(_nextState);
        }

        if (player.myCollision.IsTouchingWall())
        {
            player.movementComp.canDoubleJump = player.movementComp.hasDoubleJumpAbility;

            player.SetState(new OnWallState(_isMoving));
        }
    }
    
}
