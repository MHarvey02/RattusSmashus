using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JumpState : BaseState
{
    private bool _isMoving;

    public override void EnterState(PlayerContext player)
    {
        player.myAnimator.Play("Jumping");
        player.movementComp.Jump();
    }

    public JumpState(bool isMoving = false)
    {
        _isMoving = isMoving;
    } 

    public override void Move(InputAction.CallbackContext inputContext, PlayerContext player)
    {
        player.movementComp.SetDirection(inputContext.ReadValue<Vector2>().x);     
    }

    public override void FixedUpdate(PlayerContext player)
    {
        player.movementComp.CheckMoveSpeed();
        if (!player.myCollision.IsTouchingGround())
        {
            player.SetState(new InAirState(_isMoving));
        }
    }
}
