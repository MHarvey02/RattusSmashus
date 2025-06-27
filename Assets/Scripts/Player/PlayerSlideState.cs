using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerSlideState : PlayerBaseState
{
    public override void EnterState(PlayerContext player)
    {
        player.myAnimator.SetTrigger("isSliding");
        player.movementComp.Slide();
        ExitState(player, player.MoveState, null);
    }
    public override void Jump(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        ExitState(player, player.JumpState, null);

    }

    public override void ExitState(PlayerContext player, PlayerBaseState nextState, bool? isMovingHorizontal)
    {
        player.myAnimator.SetTrigger("StandFromSlide");
        player.SetState(nextState, isMovingHorizontal);
    }
}
