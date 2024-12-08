using System.Collections.Generic;
using UnityEngine;


public class DestinationManager : Singleton<DestinationManager>
{
    private Dictionary<string, Transform[]> _areaTransformDictionary;

    private void Awake()
    {
        InitializeOnAwake();
    }

    private void InitializeOnAwake()
    {
        _areaTransformDictionary = new Dictionary<string, Transform[]>();

        var childCount = transform.childCount;

        for(int i = 0; i < childCount; i++)
        {
            var childObject = transform.GetChild(i).gameObject;

            var name = childObject.name;

            if (!_areaTransformDictionary.ContainsKey(name))
            {
                var wayPointsCount = childObject.transform.childCount;

                _areaTransformDictionary[name] = new Transform[wayPointsCount];
            }

            for(int k = 0; k < _areaTransformDictionary[name].Length; k++)
            {
                Transform wayPointTransform = childObject.transform.GetChild(k);

                _areaTransformDictionary[name][k] = wayPointTransform;  
            }
        }
    }

    public Transform[] GetWayPoints(string areaName)
    {
        if (!_areaTransformDictionary.ContainsKey(areaName))
        {
            return null;
        }

        return _areaTransformDictionary[areaName];
    }


    private void DictionaryDeBug()
    {
        foreach (KeyValuePair<string, Transform[]> pair in _areaTransformDictionary)
        {
            var key = pair.Key;
            var value = pair.Value;

            Debug.Log($"Key = {key}");

            foreach (var childTransform in value)
            {
                Debug.Log($"childName = {childTransform.gameObject.name}");
            }
        }
    }
}