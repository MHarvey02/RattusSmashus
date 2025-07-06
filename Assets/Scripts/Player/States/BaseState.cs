using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class BaseState
{
    public virtual void EnterState(PlayerContext player)
    {
        Debug.Log(this);
    }

    public virtual void EnterState(PlayerContext player, bool? isMovingHorizontal = false){}
    
    public virtual void ExitState(PlayerContext player, BaseState nextState, bool? isMovingHorizontal){}

    //Actions
    public virtual void Move(InputAction.CallbackContext inputContext, PlayerContext player) {}
    public virtual void Jump(InputAction.CallbackContext inputContext, PlayerContext player){}
    public virtual void Slide(InputAction.CallbackContext inputContext, PlayerContext player){}

    public virtual void Shoot(InputAction.CallbackContext inputContext, PlayerContext player){}

    public virtual void Grapple(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        if (inputContext.started && player.myGrapple.currentGrapplePoint != null && player.myGrapple.hasGrapple)
        {
            player.myGrapple.currentGrapplePoint.attatch(player.movementComp.rb);
            player.SetState(player.GrappleState, null);  
        }
        
    }

    public virtual void GrapplePull(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        if (inputContext.started && player.myGrapple.currentGrapplePoint != null && player.myGrapple.hasGrapple)
        {
            player.myGrapple.pull();
        }
    }


    //Updates
    public virtual void FixedUpdate(PlayerContext player) { }
}
