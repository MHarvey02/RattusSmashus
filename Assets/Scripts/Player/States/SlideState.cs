using System.Collections;
using System.Collections.Generic;
using Enemy.Boss.States;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class SlideState : BaseState
{
    private float slideTime = 1.0f;
    private IEnumerator SlideTimeCoroutine;
    private BaseState _nextState;
    private bool _isMoving = true;
    public override void EnterState(PlayerContext player)
    {
        _nextState = new MoveState();

        player.myAnimator.Play("StartSlide");
        //player.mySounds.Slide();
        player.myMovementComp.Slide();

        SlideTimeCoroutine = SlideTime(player);
        player.StartCoroutine(SlideTimeCoroutine);
    }

    public override void Move(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        _nextState = inputContext.canceled == true ? new IdleState() : new MoveState();

        if (inputContext.canceled)
        {
            _nextState = new IdleState();
            _isMoving = false;
        }
        else
        {
            _nextState = new MoveState();
            _isMoving = true;
        }

        if (inputContext.ReadValue<Vector2>().x == player.myMovementComp.Direction * -1)
        {
            player.StartCoroutine(CancelSlide(player));
        }
        player.myMovementComp.SetDirection(inputContext.ReadValue<Vector2>().x);
    }

    public IEnumerator CancelSlide(PlayerContext player)
    {
        yield return new WaitForSecondsRealtime(1);
            player.myAnimator.Play("EndSlide");
            player.StopCoroutine(SlideTimeCoroutine);
            player.SetState(new MoveState());
    }

    public override void Jump(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        player.StopCoroutine(SlideTimeCoroutine);
        player.mySounds.Jump();
        player.SetState(new JumpState(_isMoving));
    }
    
    public IEnumerator SlideTime(PlayerContext player)
    {
        yield return new WaitForSecondsRealtime(slideTime);
        player.myAnimator.Play("EndSlide");
        player.SetState(_nextState);
    }

    public override void FixedUpdate(PlayerContext player)
    {
        //player.myMovementComp.CheckMoveSpeed();
        player.myMovementComp.HorizontalMoveInAir();
    }
}
