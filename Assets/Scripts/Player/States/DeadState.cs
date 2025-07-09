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
        player.myAnimator.Play("Dying");
        
        player.myParticleSystem.Play();
        player.StopAllCoroutines();
        //play death animation
        // I want to invoke a function here to draw text to screen on how to respawn

    }
    public override void Shoot(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        return;
    }
    public override void Grapple(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        return;
    }

    public override void GrapplePull(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        return;
    }

    public override void Slide(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        return;
    }
    public override void Jump(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        if (inputContext.started)
        {
            player.respawnEvent.Invoke();
        }
        
    }
    }
//}





