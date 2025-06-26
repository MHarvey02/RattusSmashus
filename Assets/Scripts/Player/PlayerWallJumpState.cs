using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerBaseState
{
    public override void EnterState(PlayerContext player)
    {
        player.movementComp.WallJump();
        player.SetState(player.InAirState);
    }
    

}
