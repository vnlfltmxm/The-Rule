using System;
using UnityEngine;

namespace Script.Pawn
{
    public abstract class Pawn : MonoBehaviour
    {
        private void Awake()
        {
            OnAwake();
        }
        private void Start()
        {
            OnStart();
        }
        private void Update()
        {
            OnUpdate();
        }
        private void FixedUpdate()
        {
            OnFixedUpdate();
        }
        private void LateUpdate()
        {
            OnLateUpdate();
        }

        protected virtual void OnAwake(){}
        protected virtual void OnStart(){}
        protected virtual void OnUpdate(){}
        protected virtual void OnFixedUpdate(){}
        protected virtual void OnLateUpdate(){}
    }
}