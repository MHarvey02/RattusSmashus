using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;



public class PlayerContext : MonoBehaviour
{
    #region States 
    public BaseState currentState;

    public MoveState MoveState = new MoveState();
    public IdleState IdleState = new IdleState();

    public JumpState JumpState = new JumpState();
    public InAirState InAirState = new InAirState();

    public OnWallState OnWallState = new OnWallState();
    public WallJumpState WallJumpState = new WallJumpState();

    public SlideState SlideState = new SlideState();

    public KnockbackState knockbackState = new KnockbackState();

    public DeadState DeadState = new DeadState();

    public GrappleState GrappleState = new GrappleState();

    #endregion

    public Movement movementComp;
    public Animator myAnimator;
    public Shotgun myShotgun;
    public Grapple myGrapple;

    #region Events
    public UnityEvent deathEvent;
    public UnityEvent respawnEvent;
    public UnityEvent completeLevelEvent;
    #endregion

    [SerializeField]
    public ParticleSystem myParticleSystem;

    //This needs to be changed and is only here for testing
    [SerializeField]
    public TMP_Text deadText;


    


    // Start is called before the first frame update
    void Awake()
    {
        movementComp = GetComponent<Movement>();
        myAnimator = GetComponent<Animator>();
        //myShotgun = GetComponent<Shotgun>();
        myGrapple = GetComponent<Grapple>();
        //myParticleSystem = GetComponent<ParticleSystem>();


        deathEvent.AddListener(GameManager.DrawDeadText);
        deathEvent.AddListener(TestTools.CountDeath);




        respawnEvent.AddListener(GameManager.ResetLevel);

        
        completeLevelEvent.AddListener(GameManager.LoadNextLevel);        
        completeLevelEvent.AddListener(TestTools.OnLevelEnd);

        myParticleSystem.Stop();
        SetState(IdleState);
    }

    void Start()
    {

    }

    public void GiveDoubleJump()
    {
        movementComp.canDoubleJump = true;
        movementComp.hasDoubleJumpAbility = true;
    }
    public void GiveShotgun()
    {
        myShotgun.hasShotgun = true;
    }

    public void SetState(BaseState newState)
    {

        currentState = newState;
        currentState.EnterState(this);
    }

    public void SetState(BaseState newState, bool? isMovingHorizontal)
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

    public void Grapple(InputAction.CallbackContext inputContext) => currentState.Grapple(inputContext, this);
    public void GrapplePull(InputAction.CallbackContext inputContext) => currentState.GrapplePull(inputContext, this);



    public void SkipLevel(InputAction.CallbackContext inputContext) => currentState.SkipLevel(inputContext, this);


    public void OnCollisionEnter2D(Collision2D collision) => currentState.OnCollisionEnter2D(collision, this);

    public void OnTriggerEnter2D(Collider2D collision) => currentState.OnTriggerEnter2D(collision, this);

    //Updates
    public void FixedUpdate() => currentState.FixedUpdate(this);
}




