using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrappleState : BaseState
{

    //This is currently a quick fix until I can work out why ontriggerExit is called when the player is still in the trigger during the state change, 
    // deleting the reference inside of player.myGrapple
    GrapplePoint grapplePoint;

    bool isMovingHorizontal = false;
    public override void EnterState(PlayerContext player)
    {
        grapplePoint = player.myGrapple.currentGrapplePoint;
    }

    public override void Move(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        player.movementComp.HorizontalMove();
        if (inputContext.canceled)
        {
            isMovingHorizontal = false;
            return;
        }
        isMovingHorizontal = true;
    }

    public override void Grapple(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        if (inputContext.started)
        {

            //need to calc next state
            ExitState(player, player.InAirState, isMovingHorizontal);
        }

    }

    public override void GrapplePull(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        return;
    }


    public override void Jump(InputAction.CallbackContext inputContext, PlayerContext player)
    {

        ExitState(player, player.JumpState, isMovingHorizontal);
    }

    public override void Shoot(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        return;
    }

    public override void ExitState(PlayerContext player, BaseState nextState, bool? _isMovingHorizontal)
    {
        grapplePoint.detatch();
        player.myGrapple.RemoveGrappleLine();
        player.SetState(nextState, _isMovingHorizontal);
        isMovingHorizontal = false;
    }

    //Updates
    public override void FixedUpdate(PlayerContext player)
    {

        player.myGrapple.DrawGrappleLine();
        if (player.movementComp.GroundCollisionCheck())
        {
            if (isMovingHorizontal)
            {
                ExitState(player, player.MoveState, null);
                return;
            }
            ExitState(player, player.IdleState, null);
        }

        if (player.movementComp.WallCollisionCheck())
        {
            ExitState(player, player.OnWallState, null);
        }
    }
}
