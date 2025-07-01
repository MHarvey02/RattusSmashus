using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DeadState : BaseState
{
    public override void EnterState(PlayerContext player)
    {
        //play death animation
        //This alos needs to be moved to an input
        player.deathEvent.Invoke();
    }
}
