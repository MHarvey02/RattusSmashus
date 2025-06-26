using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSlideState : PlayerBaseState
{
    public override void EnterState(PlayerContext player)
    {
        player.movementComp.Slide();
        player.SetState(player.MoveState);
    }
    public override void Jump(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        player.SetState(player.JumpState);
    }
}
