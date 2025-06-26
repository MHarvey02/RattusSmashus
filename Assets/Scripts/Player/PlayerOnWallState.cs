using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerOnWallState : PlayerBaseState
{

    public override void EnterState(PlayerContext player)
    {
        Debug.Log(this);
        player.movementComp.HitWall();
    }

    public override void Jump(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        if (inputContext.canceled)
        {
            return;
        }
        player.SetState(player.WallJumpState);
    }


    //Updates
    public override void FixedUpdate(PlayerContext player)
    {
        //Change to variable
        player.movementComp.currentMoveSpeedCap += 3 * Time.deltaTime;

        player.movementComp.HorizontalMove();
        if (player.movementComp.GroundCollisionCheck())
        {
            
            player.SetState(player.IdleState);
        }

        if (!player.movementComp.WallCollisionCheck())
        {
            player.SetState(player.InAirState,true);
        }
    }
}
