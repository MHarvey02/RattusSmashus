using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;

public class IdleState : BaseState
{
    public override void EnterState(PlayerContext player)
    {
        player.myMovementComp.resetMaxMoveSpeed();
        player.myMovementComp.rb.linearVelocity = new Vector2(0, 0);
        player.myAnimator.Play("Idle");
        if (player.myMovementComp.Direction != 0)
        {
            player.SetState(new MoveState());
        }

    }

    public override void Move(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        player.myMovementComp.SetDirection(inputContext.ReadValue<Vector2>().x);
        if (inputContext.started)
        {
            player.SetState(new MoveState());
        }
    }

    public override void Jump(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        if (inputContext.started)
        {
            player.mySounds.Jump();
            player.SetState(new JumpState());
        }

    }

    public override void Grapple(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        return;
    }
    
    //Update
    public override void FixedUpdate(PlayerContext player)
    {

        if (!player.myCollision.IsTouchingGround())
        {
            player.SetState(new InAirState(false));
        }

        if (player.myCollision.IsTouchingWall())
        {
            player.myMovementComp.resetMaxMoveSpeed();
        }
    }
    
}
