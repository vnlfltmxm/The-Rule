using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PrefabDataBase", menuName = "Scriptable Objects/PrefabDataBase")]
public class PrefabDataBase : ScriptableObject
{
    public List<GameObject> prefabs = new List<GameObject>();

    public GameObject FindPrefab(string prefabName)
    {
        foreach (var prefab in prefabs)
        {
            if (prefab.name == prefabName)
                return prefab;
        }
        Logger.LogError($"{name}에 prefabName가 {prefabName} 인 프리팹이 존재하지 않습니다.");
        return null;
    }
}
