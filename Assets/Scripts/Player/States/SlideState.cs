using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class SlideState : BaseState
{
    private float slideTime = 1.0f;
    private IEnumerator SlideTimeCoroutine;

    BaseState nextState;

    public override void EnterState(PlayerContext player)
    {
        nextState = player.MoveState;
        SlideTimeCoroutine = SlideTime(player);
        player.myAnimator.SetTrigger("isSliding");
        player.movementComp.Slide();
        player.StartCoroutine(SlideTimeCoroutine);
    }

    public override void Move(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        if (inputContext.canceled)
        {
            nextState = player.IdleState;
            return;
        }
        nextState = player.MoveState;
    }

    public override void Jump(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        player.StopCoroutine(SlideTimeCoroutine);
        ExitState(player, player.JumpState, true);

    }

    public override void Shoot(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        return;
    }

    public IEnumerator SlideTime(PlayerContext player)
    {
        yield return new WaitForSecondsRealtime(slideTime);
        player.myAnimator.SetTrigger("StandFromSlide");
        ExitState(player, nextState, null);
    }

    public override void ExitState(PlayerContext player, BaseState nextState, bool? isMovingHorizontal)
    {
        
        player.SetState(nextState, isMovingHorizontal);
    }
    public override void FixedUpdate(PlayerContext player)
    {
        player.movementComp.HorizontalMove();
    }
}
