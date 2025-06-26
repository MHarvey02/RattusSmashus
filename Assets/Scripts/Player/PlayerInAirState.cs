using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInAirState : PlayerBaseState
{
    public PlayerBaseState nextState;
    bool isMovingHorizontal = false;

    public override void EnterState(PlayerContext player)
    {
        Debug.Log(this);

        isMovingHorizontal = false;
        nextState = player.IdleState;
    }

    public override void EnterState(PlayerContext player, bool _isMovingHorizontal)
    {
        Debug.Log(this);

        isMovingHorizontal = _isMovingHorizontal;
        if (isMovingHorizontal)
        {
            nextState = player.MoveState;
        }
        else
        {
            nextState = player.IdleState;
        }
    }

    public override void Move(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        if (inputContext.canceled)
        {
            isMovingHorizontal = false;
            nextState = player.IdleState;
            return;
        }
        if (inputContext.started)
        {
            isMovingHorizontal = true;
            nextState = player.MoveState;
        }
            
    }

    public override void Jump(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        return;
    }

    //Updates
    public override void FixedUpdate(PlayerContext player)
    {
        if (isMovingHorizontal)
        {
           player.movementComp.HorizontalMove(); 
        }
        
        if (player.movementComp.GroundCollisionCheck())
        {
            
            player.SetState(nextState);
        }

        if (player.movementComp.WallCollisionCheck())
        {

            player.SetState(player.OnWallState);
        }
    }
}
