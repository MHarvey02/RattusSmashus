using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class KnockbackState : BaseState
{

    private IEnumerator _coroutine;

    private BaseState _nextState;
    private bool _isMoving;

    public KnockbackState(bool isMoving = false)
    {
        _nextState = isMoving == true ? new MoveState() : new IdleState();
        _isMoving = isMoving; 
    }
    public override void EnterState(PlayerContext player)
    {
        _coroutine = TimeBeforeStateChange(player);
        player.StartCoroutine(_coroutine);
    }

    public override void Move(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        player.movementComp.SetDirection(inputContext.ReadValue<Vector2>().x);
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


    }

    private IEnumerator TimeBeforeStateChange(PlayerContext player)
    {
        yield return new WaitForSeconds(0.5f);
        if (!player.myCollision.IsTouchingGround())
        {
            player.SetState(new InAirState(_isMoving));
        }
        else
        {
          player.SetState(_nextState);  
        }
        

    }

    public override void Shoot(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        return;
    }

}
