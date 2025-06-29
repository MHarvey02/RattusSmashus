using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerSlideState : PlayerBaseState
{
    private float slideTime = 1.0f;
    private IEnumerator SlideTimeCoroutine;

    public override void EnterState(PlayerContext player)
    {
        SlideTimeCoroutine = SlideTime(player);
        player.myAnimator.SetTrigger("isSliding");
        player.movementComp.Slide();
        player.useCoroutine(ref SlideTimeCoroutine);
    }
    public override void Jump(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        ExitState(player, player.JumpState, null);

    }

    public override void Shoot(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        return;
    }

    public IEnumerator SlideTime(PlayerContext player)
    {
        yield return new WaitForSecondsRealtime(slideTime);
        ExitState(player, player.MoveState, null);
    }

    public override void ExitState(PlayerContext player, PlayerBaseState nextState, bool? isMovingHorizontal)
    {
        player.myAnimator.SetTrigger("StandFromSlide");
        player.SetState(nextState, isMovingHorizontal);
    }
    public override void FixedUpdate(PlayerContext player)
    {
        player.movementComp.HorizontalMove();
    }
}
