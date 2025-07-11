using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class OnWallState : BaseState
{


    public override void EnterState(PlayerContext player)
    {
        player.myAnimator.SetBool("isWallSliding", true);
        player.myAnimator.Play("WallSlide");
        player.movementComp.HitWall();
    }


    public override void Jump(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        if (inputContext.canceled)
        {
            return;
        }
        if (inputContext.started)
        {
            ExitState(player, player.WallJumpState, null);
        }

    }

    public override void ExitState(PlayerContext player, BaseState nextState, bool? isMovingHorizontal)
    {

        player.myAnimator.SetBool("isWallSliding", false);
        player.SetState(nextState, isMovingHorizontal);
    }
    
    //Updates
    public override void FixedUpdate(PlayerContext player)
    {
        player.movementComp.CheckMoveSpeed();
        //Change to variable
        player.movementComp.currentMoveSpeedCap += 0.5f * Time.deltaTime;

        player.movementComp.HorizontalMove();
        if (player.movementComp.GroundCollisionCheck())
        {
            ExitState(player, player.IdleState, null);
        }
        //there is currently an issue where the play will move in their last direction when exiting the state this way
        if (!player.movementComp.WallCollisionCheck())
        {
            ExitState(player, player.InAirState, true);
        }
    }
}
