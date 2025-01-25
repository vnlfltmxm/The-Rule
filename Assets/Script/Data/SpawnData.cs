using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "SpawnData", menuName = "Scriptable Objects/SpawnData")]
public class SpawnData : ScriptableObject
{
    [Header("스폰 몬스터 프리팹")] public GameObject MonsterPrefab;

    [Header("스폰확률 시작 시간"), SerializeField]
    private float _spawnStartTime;

    [Header("시작 스폰확률"), SerializeField, Range(0f, 1f)]
    public float _startSpawnProbability;

    [Header("스폰확률 증가 여부"), SerializeField] 
    public bool _isIncreasedSpawnProbability;

    [Header("스폰확률 증가 수치"), SerializeField, Range(0f, 1f)]
    public float _increasedSpawnProbability;

    [Header("스폰확률 증가 간격"), SerializeField]
    public float _increasedProbabilityIntervalTime;

    [Header("스폰불가 조건"), SerializeField]
    public int _cantSpawnConditions;

    [Header("스폰위치"), SerializeField]
    public int _spawnPositionType;




    public float SpawnStartTime => _spawnStartTime;
    public float StartSpawnProbability => _startSpawnProbability;
    public bool IsIncreasedSpawnProbability => _isIncreasedSpawnProbability;
    public float IncreasedSpawnProbability => _increasedSpawnProbability;
    public float IncreasedProbabilityIntervalTime => _increasedProbabilityIntervalTime;
    public int CantSpawnConditions => _cantSpawnConditions;
    public int SpawnPositionType => _spawnPositionType;
}
