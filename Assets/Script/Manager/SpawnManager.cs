using System.Collections.Generic;
using Script.Util;
using UnityEngine;

public class SpawnManager : SingletonMonoBehaviour<SpawnManager>
{
    private List<GameObject> _spawnedObjects = new List<GameObject>();

    public void SpawnObject(GameObject prefab, AreaType areaType = AreaType.Null)
    {
        GameObject go = Instantiate(prefab);
        _spawnedObjects.Add(go);
    }
    public void RemoveObject(GameObject go)
    {
        _spawnedObjects.Remove(go);
        Destroy(go);
    }

    public bool HasObjectByName(string objectName)
    {
        foreach (var spawnedObject in _spawnedObjects)
        {
            if (spawnedObject.name.Contains(objectName))
                return true;
        }
        return false;
    }
}
