using System;
using Script.Pawn;
using UnityEngine;

namespace LN
{
    public abstract class State<T> where T : Pawn
    {
        protected T _stateObject;

        public State(T stateObject)
        {
            _stateObject = stateObject;
        }

        public abstract void StateEnter();
        public abstract Type StateCheck();
        public abstract void StateExit();
        
        public virtual void StateUpdate(){}
        public virtual void StateFixedUpdate(){}
        public virtual void StateLateUpdate(){}
        public virtual void OnTriggerEnter(Collider other){}
    }
}
