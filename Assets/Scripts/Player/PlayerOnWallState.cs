using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerOnWallState : PlayerBaseState
{

    public override void EnterState(PlayerContext player)
    {
        player.myAnimator.SetBool("isWallSliding", true);
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



    public override void ExitState(PlayerContext player, PlayerBaseState nextState, bool? isMovingHorizontal)
    {
        player.myAnimator.SetBool("isWallSliding", false);
        player.SetState(nextState, isMovingHorizontal);
    }
    //Updates
    public override void FixedUpdate(PlayerContext player)
    {
        //Change to variable
        player.movementComp.currentMoveSpeedCap += 3 * Time.deltaTime;

        player.movementComp.HorizontalMove();
        if (player.movementComp.GroundCollisionCheck())
        {
            ExitState(player, player.IdleState, null);
        }

        if (!player.movementComp.WallCollisionCheck())
        {   
            ExitState(player, player.InAirState, true);
        }
    }
}
