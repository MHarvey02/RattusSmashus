using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PlayerBaseState
{
    public virtual void EnterState(PlayerContext player)
    {
        Debug.Log(this);
    }

    public virtual void EnterState(PlayerContext player, bool isMovingHorizontal = false)
    {
       
    }

    //Actions
    public virtual void Move(InputAction.CallbackContext inputContext, PlayerContext player) {}
    public virtual void Jump(InputAction.CallbackContext inputContext, PlayerContext player){}
    public virtual void Slide(InputAction.CallbackContext inputContext, PlayerContext player){}

    //Updates
    public virtual void FixedUpdate(PlayerContext player) { }
}
