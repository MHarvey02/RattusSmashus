using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJumpState : PlayerBaseState
{
    bool isMovingHorizontal = false;

    public override void EnterState(PlayerContext player)
    {
        Debug.Log(this);
        isMovingHorizontal = false;
        player.movementComp.Jump();

    }

    public override void EnterState(PlayerContext player, bool _isMovingHorizontal)
    {
        Debug.Log(this);
        isMovingHorizontal = false;
        player.movementComp.Jump();
        isMovingHorizontal = _isMovingHorizontal;

    }

    public override void Jump(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        return;
    }

    public override void FixedUpdate(PlayerContext player)
    {
        if (player.movementComp.GroundCollisionCheck())
        {
           player.SetState(player.InAirState, isMovingHorizontal); 
        }
    }
}
