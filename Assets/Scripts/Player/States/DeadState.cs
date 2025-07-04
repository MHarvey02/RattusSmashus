using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



//namespace Player.States
//{
public class DeadState : BaseState
{
    public override void EnterState(PlayerContext player)
    {
        //play death animation
        // I want to invoke a function here to draw text to screen on how to respawn
        
    }

    public override void Jump(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        player.respawnEvent.Invoke();
    }
    }
//}





