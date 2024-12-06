using System;
using UnityEngine;

namespace Script.Pawn
{
    public abstract class Pawn : MonoBehaviour
    {
        private void Start()
        {
            Init();
        }

        protected abstract void Init();
    }
}