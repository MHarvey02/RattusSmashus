using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class DeadState : BaseState
{
    public override void EnterState(PlayerContext player)
    {
        player.myAnimator.Play("DeathRedo");
        player.mySounds.Death();
        player.deadText.enabled = true;
        player.myBloodEffect.Play();
        player.StopAllCoroutines();
        LevelResultsScreen.mostRecentDeathCount++;

    }
   //Remove the ability to do anything
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
    //Respawn the player
    public override void Jump(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        if (inputContext.started)
        {
            player.respawnEvent.Invoke();
        }

    }

    public override void OnTriggerEnter2D(Collider2D collision, PlayerContext player)
    {
        return;
    }

    public override void OnCollisionEnter2D(Collision2D collision, PlayerContext player)
    {
        return;
    }
    //Skip the level
    public override void SkipLevel(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        if (inputContext.started)
        {
            player.completeLevelEvent.Invoke();
            return;
        }
        
    }

}





