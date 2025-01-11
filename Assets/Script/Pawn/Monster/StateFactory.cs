using System;
using System.Collections.Generic;
using LN;
using Script.Pawn;
using UnityEngine;

namespace LN
{
    public static class StateFactory
    {
        public static TState CreateState<TState>(Pawn pawn) where TState : State, new()
        {
            TState state = new TState();
            state.InitState(pawn);
            return state;
        }
    }
}