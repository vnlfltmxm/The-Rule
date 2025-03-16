using System;
using System.Collections.Generic;
using Script.Util;
using UnityEngine;

public class HierarchyManager : SingletonMonoBehaviour<HierarchyManager>
{
    private List<HierarchyObject> _hierarchyObjects = new List<HierarchyObject>();
    protected override void OnAwake()
    {
        foreach (HierarchyParentType type in Enum.GetValues(typeof(HierarchyParentType)))
        {
            GameObject parentObject = GameObject.Find(type.ToString());
            if (parentObject == null)
                parentObject = new GameObject(type.ToString());
            _hierarchyObjects.Add(new HierarchyObject(){ParentType = type, Parent = parentObject.transform});
        }
    }

    public void SetParent(Transform child, HierarchyParentType parentType)
    {
        Transform parent = GetParent(parentType);
        child.SetParent(parent);
    }
    
    public Transform GetParent(HierarchyParentType parentType)
    {
        foreach (HierarchyObject hierarchyObject in _hierarchyObjects)
        {
            if (hierarchyObject.ParentType == parentType)
                return hierarchyObject.Parent;
        }
        return null;
    }
}

public struct HierarchyObject
{
    public HierarchyParentType ParentType;
    public Transform Parent;
}