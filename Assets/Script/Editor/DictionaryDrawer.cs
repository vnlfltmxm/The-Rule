using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Codice.CM.WorkspaceServer;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Dictionary<,>))]
public class DictionaryDrawer : PropertyDrawer
{
    private static readonly float FOLDOUT_HEIGHT = 16f;
    private static readonly float ELEMENT_HEIGHT = EditorGUIUtility.singleLineHeight; // Dictionary 요소 높이
    private static readonly float SPACING = 2f; // 요소 간의 간격
    
    private IDictionary dictionary;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (dictionary == null)
            dictionary = property.GetValue<IDictionary>();

        float height = FOLDOUT_HEIGHT;
        if (property.isExpanded && dictionary != null)
        {
            foreach (var key in dictionary.Keys)
            {
                // 각 Dictionary 요소마다 높이 추가
                height += ELEMENT_HEIGHT + SPACING;
            }
        }
        return height;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (dictionary == null)
            dictionary = property.GetValue<IDictionary>();
        if (dictionary == null)
        {
            EditorGUI.LabelField(position, "Dictionary is null");
            return;
        }
        
        
        // 제목 표시
        position.height = EditorGUIUtility.singleLineHeight;
        EditorGUI.LabelField(position, label);

        // Dictionary 항목 표시
        position.y += EditorGUIUtility.singleLineHeight;

        if (property.isExpanded)
        {
            foreach (DictionaryEntry entry in dictionary)
            {
                var key = entry.Key;
                var value = entry.Value;

                // 각 키와 값 표시
                var keyRect = new Rect(position.x, position.y, position.width * 0.4f, position.height);
                var valueRect = new Rect(position.x + position.width * 0.5f, position.y, position.width * 0.5f,
                    position.height);

                EditorGUI.LabelField(keyRect, key != null ? key.ToString() : "null");
                if (value is int intValue)
                {
                    dictionary[key] = EditorGUI.IntField(valueRect, intValue);
                }
                else if (value is float floatValue)
                {
                    dictionary[key] = EditorGUI.FloatField(valueRect, floatValue);
                }
                else if (value is string stringValue)
                {
                    dictionary[key] = EditorGUI.TextField(valueRect, stringValue);
                }
                else if (value is UnityEngine.Object objValue)
                {
                    dictionary[key] = EditorGUI.ObjectField(valueRect, objValue, typeof(UnityEngine.Object), true);
                }

                position.y += EditorGUIUtility.singleLineHeight;
            }
        }
        // 변경 사항 저장
        property.serializedObject.ApplyModifiedProperties();
        
        /*EditorGUI.BeginProperty(position, label, property);
        Rect foldoutRect = new Rect(position.x, position.y, position.width, FOLDOUT_HEIGHT);
        property.isExpanded = EditorGUI.Foldout(foldoutRect, property.isExpanded, label);

        if (property.isExpanded)
        {
            float addY = FOLDOUT_HEIGHT;
            for (int i = 0; i < dictionary.arraySize; i++)
            {
                Rect rect = new Rect(position.x, position.y + addY, position.width, EditorGUI.GetPropertyHeight(dictionary.GetArrayElementAtIndex(i)));
                addY += rect.height;
                EditorGUI.PropertyField(rect, dictionary.GetArrayElementAtIndex(i), new GUIContent(dictionary.GetArrayElementAtIndex(i).ToString()), true);
            }
        }
        EditorGUI.EndProperty();*/
    }
}