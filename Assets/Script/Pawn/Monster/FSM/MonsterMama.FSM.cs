using System;
using System.Collections.Generic;
using LN;
using Script.Pawn;
using UnityEngine;

public partial class MonsterMama
{
    protected override void InitFSM()
    {
        AddState<IdleState>(this);
        AddState<LookState>(this);
        AddState<PatrolState>(this);
        AddState<SoundTraceState>(this);
        AddState<TrackingState>(this);

        ChangeState(typeof(IdleState));
    }
}
