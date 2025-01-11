using System;
using System.Collections.Generic;
using LN;
using Script.Pawn;
using UnityEngine;

public partial class MonsterBase : Pawn
{
    [Header("MyPatrolArea")]
    [SerializeField] private string _areaName;
    [Header("WaitTime")]
    [SerializeField] private float _waitTime;
    [Header("RotationSpeed")]
    [SerializeField] private float _rotationSpeed;
    [Header("ObjectSpeed")]
    [SerializeField] private float _objectSpeed;
    [Header("Hear")]
    [SerializeField] private float _soundDistance;
    [Header("ViewDistance")]
    [SerializeField] private float _viewDistance;
    [Header("ViewAngle")]
    [SerializeField] private float _viewAngle;

    public string AreaName => _areaName;
    public float WaitTime => _waitTime;
    public float RotationSpeed => _rotationSpeed;
    public float ObjectSpeed => _objectSpeed;
    public float ViewDistance => _viewDistance;
    public float ViewAngle => _viewAngle;
    

    #region Gizmo
    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
            return;

        foreach (var state in _stateDictionary.Values)
        {
            state.AlwaysDrawGizmos();
        }
        _currentState.OnStateDrawGizmos();
    }
    #endregion

    #region 가상함수
    protected override void OnAwake()
    {
    }
    protected override void OnStart()
    {
        InitFSM();
    }
    protected override void OnUpdate()
    {
        UpdateFSM();
    }
    protected override void OnFixedUpdate()
    {
        FixedUpdateFSM();
    }
    protected override void OnLateUpdate()
    {
        LateUpdateFSM();
    }
    #endregion
    
}
