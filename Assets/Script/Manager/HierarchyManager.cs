using System.Collections.Generic;
using UnityEngine;

public static class HierarchyManager
{
    private static Dictionary<string, Transform> _parentDictionary = new Dictionary<string, Transform>();

    public static Transform GetParent(string name)
    {
        if (_parentDictionary.ContainsKey(name) == false)
            _parentDictionary.Add(name, new GameObject($"{name}_Parent").transform);
        return _parentDictionary[name];
    }
}