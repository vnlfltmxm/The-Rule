using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : Singleton<EnemyManager>
{
    private Dictionary<EnemyType, IEnemy> _enemyDictionary
         = new Dictionary<EnemyType, IEnemy>();

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
}
