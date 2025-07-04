using System.Collections;
using System.Collections.Generic;
using Enemy.Melee.States;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;
using UnityEngine.Rendering;


namespace Enemy.Melee
{
    public class StateContext : MonoBehaviour
    {
        Base _currentState;

        #region States
        public Patrolling Patrolling = new();
        public Attacking Attacking = new();
        #endregion

        [SerializeField]
        public FollowPath Path;

        [SerializeField]
        public float TimeBetweenAttacks;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            SetState(Patrolling);
        }

        public void SetState(Base nextState)
        {
            _currentState = nextState;
            _currentState.EnterState(this);
        }


        // Update is called once per frame
        void Update() => _currentState.Update(this);

    }        
}