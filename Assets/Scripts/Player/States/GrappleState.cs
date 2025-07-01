using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrappleState : BaseState
{

    //This is currently a quick fix until I can work out why ontriggerExit is called when the player is still in the trigger during the state change, 
    // deleting the reference inside of player.myGrapple
    GrapplePoint grapplePoint;
    public override void EnterState(PlayerContext player)
    {
        grapplePoint = player.myGrapple.currentGrapplePoint;
    }

    public override void Grapple(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        if (inputContext.started)
        {
            grapplePoint.detatch();
            //need to calc next state
            ExitState(player, player.InAirState, null);
        }

    }

    public override void GrapplePull(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        player.myGrapple.pull(grapplePoint);
        grapplePoint.detatch();
        ExitState(player, player.InAirState, null);
    }


    public override void Jump(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        grapplePoint.detatch();
        ExitState(player, player.JumpState, null);
    }

    public override void ExitState(PlayerContext player, BaseState nextState, bool? isMovingHorizontal)
    {
        player.SetState(nextState, null);
    }

    //Updates
    public override void FixedUpdate(PlayerContext player)
    {

        player.movementComp.HorizontalMove();
        

        if (player.movementComp.GroundCollisionCheck())
        {
            grapplePoint.detatch();
            ExitState(player, player.IdleState, null);
        }
    }
}
