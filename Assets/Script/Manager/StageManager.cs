using System;
using System.Collections.Generic;
using NUnit.Framework;
using Script.Manager.Framework;
using UnityEngine;

public class StageManager : SingletonMonoBehaviour<StageManager>
{
    [SerializeField] private List<SpawnData> _stageSpawnDatas = new List<SpawnData>();
    
    private List<SpawnBehavior> _spawnBehaviors = new List<SpawnBehavior>();
    private bool _isInit = false;
    
    public void InitStage()
    {
        foreach (var spawnData in _stageSpawnDatas)
        {
            _spawnBehaviors.Add(new SpawnBehavior(spawnData));
        }
        _isInit = true;
    }

    private void Update()
    {
        if (_isInit == false)
            return;
        float deltaTime = Time.deltaTime;
        foreach (var spawnBehavior in _spawnBehaviors)
        {
            spawnBehavior.OnUpdateSpawn(deltaTime);
        }
    }
}

public class SpawnBehavior
{
    private bool _isSpawned = false;        //이미 스폰했는가
    //private bool _isStartBehavior;  //스폰행동이 동작중인가
    
    
    private float _cumulativeTime; //누적시간
    private float _currentSpawnProbability; //현재 스폰확률
    
    private SpawnData _spawnData;

    public SpawnBehavior(SpawnData spawnData)
    {
        _spawnData = spawnData;
        if (_spawnData.SpawnStartTime == 0f)
            _currentSpawnProbability = _spawnData.StartSpawnProbability;
        else
            _currentSpawnProbability = 0f;
    }

    public void OnUpdateSpawn(float deltaTime)
    {
        if (_isSpawned == true)
            return;
        
        //스폰불가 조건을 넘기지 못하면 중단
        //if (_spawnData.CantSpawnConditions == 0)
        //    return;
        
        //누적시간
        _cumulativeTime += deltaTime;
        
        //스폰 시작 시간을 넘기지 못하면 중단
        if (_spawnData.SpawnStartTime > _cumulativeTime)
            return;

        if (_spawnData.IsIncreasedSpawnProbability == true)
        {
            //스폰확률 증가 간격을 넘기면 확률 증가
            if (_cumulativeTime - _spawnData.SpawnStartTime > _spawnData.IncreasedProbabilityIntervalTime)
            {
                _cumulativeTime -= _spawnData.IncreasedProbabilityIntervalTime;
                _currentSpawnProbability += _spawnData.IncreasedSpawnProbability;
            }
        }
        
        float randomValue = Utility.Random.GetRandomFloat();
        
        //스폰확률에 들어왔을 시 스폰
        if (_currentSpawnProbability > randomValue)
        {
            UnityEngine.Object.Instantiate(_spawnData.MonsterPrefab);
            _isSpawned = true;
        }
    }
}
