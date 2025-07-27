using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;

public class IdleState : BaseState
{
    public override void EnterState(PlayerContext player)
    {
        player.movementComp.resetMaxMoveSpeed();
        player.movementComp.rb.linearVelocity = new Vector2(0, 0);
        player.myAnimator.Play("Idle");

    }

    public override void Move(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        player.movementComp.SetDirection(inputContext.ReadValue<Vector2>().x);
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
    
}
