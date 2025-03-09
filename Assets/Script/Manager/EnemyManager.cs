using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : Singleton<EnemyManager>
{
    private Dictionary<EnemyType, IEnemy> _enemyDictionary
         = new Dictionary<EnemyType, IEnemy>();

    private List<InvadeObject> _stationWorkerList
        = new List<InvadeObject>();

    public List<InvadeObject> StationWorkerList => _stationWorkerList;

    public void RegisterEnemy(EnemyType type, IEnemy enemy)
    {
        if (!_enemyDictionary.ContainsKey(type))
        {
            _enemyDictionary.Add(type, enemy);
        }
    }    

    public IEnemy GetEnemy(EnemyType type)
    {
        if (_enemyDictionary.TryGetValue(type, out IEnemy enemy))
            return enemy;

        return null;
    }

    public void SetStationWorker(InvadeObject invadeObject)
    {
        _stationWorkerList.Add(invadeObject);
    }

    public void StationWorkerCatchPlayer(InvadeObject invadeObject)
    {
        foreach(var worker in _stationWorkerList)
        {
            if (!invadeObject.Equals(worker))
            {
                worker.SystemTracking = false;

                worker.StopTarcking();
            }
        }

        invadeObject.SystemTracking = false;
    }
}
