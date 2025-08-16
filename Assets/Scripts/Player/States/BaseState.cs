using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public abstract class BaseState
{
    //This will be used to start any sounds, animations, etc when entering the state
    public virtual void EnterState(PlayerContext player)
    {
    }
 
    //Actions
    public virtual void Move(InputAction.CallbackContext inputContext, PlayerContext player) {}
    public virtual void Jump(InputAction.CallbackContext inputContext, PlayerContext player){}
    public virtual void Slide(InputAction.CallbackContext inputContext, PlayerContext player){}

    //Attatch to a grapple point
    public virtual void Grapple(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        //Check that the player has the grapple
        if (inputContext.started && player.myGrapple.hasGrapple)
        {
            //Check there is a hook
            GrapplePoint grapplePoint = player.myGrapple.GetHook();
            if (grapplePoint)
            {
                player.myGrapple.currentGrapplePoint.Attatch(player.myMovementComp.rb);
                player.SetState(new GrappleState());
            }
        }
    }
    //Move the player quickly towards and past the grapple point
    public virtual void GrapplePull(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        //Check that the player has the grapple
        if (inputContext.started && player.myGrapple.hasGrapple)
        {
            GrapplePoint grapplePoint = player.myGrapple.GetHook();
            //Check there is a hook
            if (grapplePoint)
            {
                player.myGrapple.Pull();
            }
        }
    }

    public virtual void Shoot(InputAction.CallbackContext inputContext, PlayerContext player) { }
    //Do anything needed before leaving the state
    //Stop animations, play sound etc
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
        player.completeLevelEvent.Invoke();
        return;
    }

    //Gives a way to return to the menu
    public virtual void ReturnToMenu(InputAction.CallbackContext inputContext)
    {
        LevelResultsScreen.nextLevel = 0;
        SceneManager.LoadScene(0);
    }

    //Updates
    public virtual void FixedUpdate(PlayerContext player) { }
}
