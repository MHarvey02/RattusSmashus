using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JumpState : BaseState
{
    bool? isMovingHorizontal = false;

    public override void EnterState(PlayerContext player)
    {
        player.myAnimator.Play("Jumping");
        isMovingHorizontal = false;
        player.movementComp.Jump();
        
        ExitState(player, player.InAirState, null);
    }

    public override void EnterState(PlayerContext player, bool? _isMovingHorizontal)
    {
        isMovingHorizontal = false;
        player.movementComp.Jump();
        isMovingHorizontal = _isMovingHorizontal;
    
        ExitState(player, player.InAirState, isMovingHorizontal);
    }

    public override void Jump(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        return;
    }

    public override void ExitState(PlayerContext player, BaseState nextState, bool? isMovingHorizontal)
    {

        player.SetState(nextState, isMovingHorizontal);
    }

    public override void FixedUpdate(PlayerContext player)
    {

    }
}
