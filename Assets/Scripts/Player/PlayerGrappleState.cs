using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGrappleState : PlayerBaseState
{
    public override void Jump(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        player.SetState(player.JumpState);
    }
}
