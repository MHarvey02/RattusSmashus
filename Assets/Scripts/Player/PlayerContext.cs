using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
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

    public KnockbackState knockbackState = new KnockbackState();

    private PlayerDeadState playerDeadState = new PlayerDeadState();

    #endregion

    public Movement movementComp;
    public Animator myAnimator;
    public Shotgun myShotgun;

    public UnityEvent deathEvent;
    public UnityEvent completeLevelEvent;

    // Start is called before the first frame update
    void Awake()
    {
        movementComp = GetComponent<Movement>();
        myAnimator = GetComponent<Animator>();
        myShotgun = GetComponent<Shotgun>();



        int GMInt = FindObjectsByType<GameManager>(FindObjectsSortMode.None).Length;
        Debug.Log(GMInt);

        deathEvent.AddListener(GameManager.ResetLevel);
        completeLevelEvent.AddListener(GameManager.LoadNextLevel);
        SetState(IdleState);
    }

    void Start()
    {
        
    }

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
    public void Shoot(InputAction.CallbackContext inputContext) => currentState.Shoot(inputContext, this);

    //Coroutines
    public void useCoroutine(ref IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Trap")
        {
            SetState(playerDeadState, null);
            //Once the player has pressed a button reset the level in the game manager
        }
        if (collision.gameObject.tag == "Exit")
        {
            completeLevelEvent.Invoke();
            //load next level from game manager here
        }
    }


    //Updates
    public void FixedUpdate() => currentState.FixedUpdate(this); 
}
