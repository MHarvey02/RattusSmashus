using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class BaseState
{
    public virtual void EnterState(PlayerContext player)
    {
    }
 
    //Actions
    public virtual void Move(InputAction.CallbackContext inputContext, PlayerContext player) {}
    public virtual void Jump(InputAction.CallbackContext inputContext, PlayerContext player){}
    public virtual void Slide(InputAction.CallbackContext inputContext, PlayerContext player){}

    public virtual void Grapple(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        if (inputContext.started &&  player.myGrapple.hasGrapple)
        {
            GrapplePoint grapplePoint = player.myGrapple.GetHook();
            if (grapplePoint)
            {
                player.myGrapple.currentGrapplePoint.Attatch(player.movementComp.rb);
                player.SetState(new GrappleState()); 
            }
                 
        }
        
    }

    public virtual void GrapplePull(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        if (inputContext.started  && player.myGrapple.hasGrapple)
        {
            GrapplePoint grapplePoint = player.myGrapple.GetHook();
            if (grapplePoint)
            {
                player.myGrapple.Pull();
            }
        }
    }

    public virtual void Shoot(InputAction.CallbackContext inputContext, PlayerContext player) { }

    public virtual void ExitState(PlayerContext player) { return; }

    public virtual void OnTriggerEnter2D(Collider2D collision, PlayerContext player)
    {
        if (collision.gameObject.tag == "Exit")
        {
            player.completeLevelEvent.Invoke();

        }

        if (collision.gameObject.tag == "Boots")
        {
            player.GiveDoubleJump();
        }
        if (collision.gameObject.tag == "Shotgun")
        {
            player.GiveShotgun();
        }
    }

    public virtual void OnCollisionEnter2D(Collision2D collision, PlayerContext player)
    {

        if (collision.gameObject.tag == "Trap" || collision.gameObject.tag == "Boss")
        {

            player.SetState(new DeadState());
            player.deathEvent.Invoke();

        }
        if (collision.gameObject.tag == "Shotgun")
        {
            player.GiveShotgun();
        }
    }


    // This is here for testing purposes
    public virtual void SkipLevel(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        return;
    }

    //Updates
    public virtual void FixedUpdate(PlayerContext player) { }
}
