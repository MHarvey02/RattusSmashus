using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDeadState : PlayerBaseState
{
    public override void EnterState(PlayerContext player)
    {
        //play death animation
        Debug.Log("feaf");
        player.deathEvent.Invoke();
    }
}
