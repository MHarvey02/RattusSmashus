using System;
using Enemy.Melee;
using UnityEngine;

namespace Enemy.Melee.States
{
    public abstract class Base
    {
        public virtual void EnterState(StateContext enemy)
        {
            Debug.Log(this);
        }

        public virtual void Update(StateContext enemy) { }
    }
}
