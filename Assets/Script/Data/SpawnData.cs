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

    [Header("스폰불가 타입"), SerializeField]
    private Condition _cantSpawnConditionType;
    
    [Header("스폰불가 내용"), SerializeField]
    private string _cantSpawnConditionContext;

    [Header("스폰위치"), SerializeField]
    private int _spawnPositionType;




    public float SpawnStartTime => _spawnStartTime;
    public float StartSpawnProbability => _startSpawnProbability;
    public bool IsIncreasedSpawnProbability => _isIncreasedSpawnProbability;
    public float IncreasedSpawnProbability => _increasedSpawnProbability;
    public float IncreasedProbabilityIntervalTime => _increasedProbabilityIntervalTime;
    public Condition CantSpawnConditionType => _cantSpawnConditionType;
    public string CantSpawnConditionContext => _cantSpawnConditionContext;
    public int SpawnPositionType => _spawnPositionType;
}
