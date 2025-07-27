using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerContext : MonoBehaviour
{

    public BaseState currentState = new IdleState();
    public Movement movementComp;
    public PlayerCollision myCollision;
    public Animator myAnimator;
    public Shotgun myShotgun;
    public Grapple myGrapple;
    [SerializeField]
    public PlayerSounds mySounds;
    #region Events
    public UnityEvent deathEvent;
    public UnityEvent respawnEvent;
    [SerializeField]
    public UnityEvent completeLevelEvent;
    #endregion

    [SerializeField]
    public ParticleSystem myBloodEffect;

    [SerializeField]
    public ParticleSystem myRunEffect;

    //This needs to be changed and is only here for testing
    [SerializeField]
    public TMP_Text deadText;


    // Start is called before the first frame update
    void Awake()
    {
        deathEvent.AddListener(GameManager.DrawDeadText);
        deathEvent.AddListener(TestTools.CountDeath);

        respawnEvent.AddListener(GameManager.ResetLevel);
        completeLevelEvent.AddListener(TestTools.OnLevelEnd);
        completeLevelEvent.AddListener(GameManager.LoadNextLevel);        
        

        myBloodEffect.Stop();
        myRunEffect.Stop();
        
    }

    void Start()
    {
        currentState.EnterState(this);
    }

    public void SetState(BaseState newState)
    {
        currentState.ExitState(this);
        currentState = newState;
        Debug.Log(currentState);
        currentState.EnterState(this);
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

    //Actions
    public void Move(InputAction.CallbackContext inputContext) => currentState.Move(inputContext, this);

    public void Jump(InputAction.CallbackContext inputContext) => currentState.Jump(inputContext, this);

    public void Slide(InputAction.CallbackContext inputContext) => currentState.Slide(inputContext, this);

    public void Grapple(InputAction.CallbackContext inputContext) => currentState.Grapple(inputContext, this);

    public void GrapplePull(InputAction.CallbackContext inputContext) => currentState.GrapplePull(inputContext, this);


    public void Shoot(InputAction.CallbackContext inputContext) => currentState.Shoot(inputContext, this);

    public void SkipLevel(InputAction.CallbackContext inputContext) => currentState.SkipLevel(inputContext, this);

    public void OnCollisionEnter2D(Collision2D collision) => currentState.OnCollisionEnter2D(collision, this);

    public void OnTriggerEnter2D(Collider2D collision) => currentState.OnTriggerEnter2D(collision, this);
    //Updates
    public void FixedUpdate() => currentState.FixedUpdate(this);
}




