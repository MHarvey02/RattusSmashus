using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class KnockbackState : BaseState
{

    IEnumerator coroutine;

    BaseState nextState;
    public override void EnterState(PlayerContext player)
    {
        nextState = player.IdleState;
        coroutine = TimeBeforeStateChange(player);
        player.StartCoroutine(coroutine);
    }
    public override void EnterState(PlayerContext player, bool? isMovingHorizontal = false)
    {
        nextState = player.IdleState;
        coroutine = TimeBeforeStateChange(player);
        player.StartCoroutine(coroutine);
    }

    public override void Move(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        if (inputContext.canceled)
        {
            nextState = player.IdleState;
        }
        nextState = player.MoveState;
    }


    private IEnumerator TimeBeforeStateChange(PlayerContext player)
    {
        yield return new WaitForSeconds(0.5f);
        player.SetState(nextState, null);

    }

    public override void Shoot(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        return;
    }


}
