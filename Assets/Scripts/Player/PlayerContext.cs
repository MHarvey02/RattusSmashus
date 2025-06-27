using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerContext : MonoBehaviour
{
    #region States 
    public PlayerBaseState currentState;

    public PlayerMoveState MoveState = new PlayerMoveState();
    public PlayerIdleState IdleState = new PlayerIdleState();

    public PlayerJumpState JumpState = new PlayerJumpState();
    public PlayerInAirState InAirState = new PlayerInAirState();

    public PlayerOnWallState OnWallState = new PlayerOnWallState();
    public PlayerWallJumpState WallJumpState = new PlayerWallJumpState();

    public PlayerSlideState SlideState = new PlayerSlideState();
    #endregion

    public Movement movementComp;
    public Animator myAnimator;

    // Start is called before the first frame update
    void Awake()
    {
        
        movementComp = GetComponent<Movement>();

        myAnimator = GetComponent<Animator>();
        SetState(IdleState);
    }

    void Start(){}


    public void SetState(PlayerBaseState newState)
    {   
        currentState = newState;
        currentState.EnterState(this);
    }

    public void SetState(PlayerBaseState newState, bool? isMovingHorizontal)
    {
        currentState = newState;
        if (isMovingHorizontal == null)
        {
            currentState.EnterState(this);
            return;
        }
        currentState.EnterState(this, isMovingHorizontal);
    }

    //Actions
    public void Move(InputAction.CallbackContext inputContext) => currentState.Move(inputContext, this);
    public void Jump(InputAction.CallbackContext inputContext) => currentState.Jump(inputContext, this);
    public void Slide(InputAction.CallbackContext inputContext) => currentState.Slide(inputContext, this);
    
    //Updates
    public void FixedUpdate() => currentState.FixedUpdate(this); 
}
