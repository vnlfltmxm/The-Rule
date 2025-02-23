using System;
using System.Collections.Generic;
using Script.Util;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "SpawnData", menuName = "Scriptable Objects/SpawnData")]
public class SpawnData : ScriptableObject
{
    [Header("스폰 몬스터 프리팹")] public GameObject MonsterPrefab;

    [Header("스폰확률 시작 시간"), SerializeField]
    private float _spawnStartTime;

    [Header("시작 스폰확률"), SerializeField, Range(0f, 1f)]
    private float _startSpawnProbability;

    [Header("스폰확률 증가 여부"), SerializeField] 
    private bool _isIncreasedSpawnProbability;

    [Header("스폰확률 증가 수치"), SerializeField, Range(0f, 1f)]
    private float _increasedSpawnProbability;

    [Header("스폰확률 증가 간격"), SerializeField]
    private float _increasedProbabilityIntervalTime;

    [Header("스폰불가 조건"), SerializeField] 
    private List<SpawnCondition> _cantSpawnConditions;

    [Header("스폰위치"), SerializeField]
    private AreaType _spawnAreaType;
    
    [Header("올바른 규칙"), SerializeField]
    private string _correctRuleContext;
    
    [Header("틀린 규칙"), SerializeField]
    private string _wrongRuleContext;




    public float SpawnStartTime => _spawnStartTime;
    public float StartSpawnProbability => _startSpawnProbability;
    public bool IsIncreasedSpawnProbability => _isIncreasedSpawnProbability;
    public float IncreasedSpawnProbability => _increasedSpawnProbability;
    public float IncreasedProbabilityIntervalTime => _increasedProbabilityIntervalTime;
    public List<SpawnCondition> CantSpawnConditions => _cantSpawnConditions;
    public AreaType SpawnAreaType => _spawnAreaType;
    public string CorrectRuleContext => _correctRuleContext;
    public string WrongRuleContext => _wrongRuleContext;
}

[Serializable]
public struct SpawnCondition
{
    [field: SerializeField]
    public Condition CantSpawnConditionType { get; private set; }
    [field: SerializeField]
    public string CantSpawnConditionContext { get; private set; }
}
