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

    private BaseState _currentState = new IdleState();
    public Movement myMovementComp;
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

    #region Particle Systems
    [SerializeField]
    public ParticleSystem myBloodEffect;

    [SerializeField]
    public ParticleSystem myRunEffect;

    [SerializeField]
    public ParticleSystem myDoubleJumpEffect;
    #endregion
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
        myDoubleJumpEffect.Stop();
        myAnimator.enabled = true;
        
    }

    void Start()
    {
        _currentState.EnterState(this);
    }

    public void SetState(BaseState newState)
    {
        _currentState.ExitState(this);
        _currentState = newState;
        _currentState.EnterState(this);
    }

    public void GiveDoubleJump()
    {
        myMovementComp.canDoubleJump = true;
        myMovementComp.hasDoubleJumpAbility = true;
    }

    public void GiveShotgun()
    {
        myShotgun.hasShotgun = true;
    }

    //Actions
    public void Move(InputAction.CallbackContext inputContext) => _currentState.Move(inputContext, this);

    public void Jump(InputAction.CallbackContext inputContext) => _currentState.Jump(inputContext, this);

    public void Slide(InputAction.CallbackContext inputContext) => _currentState.Slide(inputContext, this);

    public void Grapple(InputAction.CallbackContext inputContext) => _currentState.Grapple(inputContext, this);

    public void GrapplePull(InputAction.CallbackContext inputContext) => _currentState.GrapplePull(inputContext, this);

    public void Shoot(InputAction.CallbackContext inputContext) => _currentState.Shoot(inputContext, this);

    public void SkipLevel(InputAction.CallbackContext inputContext) => _currentState.SkipLevel(inputContext, this);

    public void OnCollisionEnter2D(Collision2D collision) => _currentState.OnCollisionEnter2D(collision, this);

    public void OnTriggerEnter2D(Collider2D collision) => _currentState.OnTriggerEnter2D(collision, this);
    //Updates
    public void FixedUpdate() => _currentState.FixedUpdate(this);
}




