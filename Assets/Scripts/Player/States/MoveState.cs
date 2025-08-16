using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveState : BaseState
{
    public override void EnterState(PlayerContext player)
    {
        player.myAnimator.Play("Running");
        player.myRunEffect.Play();
        player.myCollision.ChangeDirection(player.myMovementComp.Direction);
        if (player.myMovementComp.Direction == 0)
        {
            player.SetState(new IdleState());
        }
    }

    public override void Move(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        player.myMovementComp.SetDirection(inputContext.ReadValue<Vector2>().x);
        if (inputContext.canceled)
        {
            player.SetState(new IdleState());
        }
    }

    public override void Jump(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        if (inputContext.started)
        {
            player.mySounds.Jump();
            player.SetState(new JumpState(true)); 
        }
    }

    public override void Slide(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        if (inputContext.started)
        {
           player.SetState(new SlideState()); 
        }
    }

    public override void Grapple(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        return;
    }

    public override void Shoot(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        if (inputContext.started && player.myShotgun.canShoot)
        {
          player.SetState(new KnockbackState(true));  
        }
    }

    public override void ExitState(PlayerContext player)
    {
        player.myRunEffect.Stop();
    }

    //Update
    public override void FixedUpdate(PlayerContext player)
    {

        if (!player.myCollision.IsTouchingGround())
        {
            player.SetState(new InAirState(true));
        }

        if (player.myCollision.IsTouchingWall())
        {
            player.myMovementComp.resetMaxMoveSpeed();
        }
        player.myMovementComp.HorizontalMove();
        //player.myMovementComp.CheckMoveSpeed();
    }

}
