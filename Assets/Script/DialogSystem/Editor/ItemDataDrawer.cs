#if UNITY_EDITOR
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ItemData))]
public class ItemDataDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // 대상 객체 가져오기
        object targetObject = GetTargetObjectOfProperty(property);
        
        if (targetObject is ItemData itemData)
        {
            string displayName = $"ID: {itemData.ID}, Name: {itemData.Name}";
            label.text = displayName;
        }
        
        EditorGUI.PropertyField(position, property, label, true);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, true);
    }
    
    // SerializedProperty에서 실제 객체 가져오기
    private object GetTargetObjectOfProperty(SerializedProperty prop)
    {
        object target = prop.serializedObject.targetObject;
        string[] path = prop.propertyPath.Split('.');

        foreach (var fieldName in path)
        {
            if (fieldName == "Array")
                continue;
            if (fieldName.StartsWith("data["))
            {
                int index = int.Parse(fieldName.Substring(5, fieldName.Length - 6));
                if (target is IList<ItemData> list)
                {
                    target = list[index];
                }
            }
            else
            {
                FieldInfo field = target.GetType().GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                if (field != null)
                    target = field.GetValue(target);
            }
        }
        return target;
    }
}
#endif