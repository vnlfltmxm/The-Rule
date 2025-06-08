using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

[CustomEditor(typeof(StateMachineData))]
public class StateMachineDataEditor : Editor
{
    //private StateMachineData _stateMachineData;
    //private SerializedProperty _stateElementsProp;
    //private SerializedProperty _globalFieldDictionaryProp;
    private void OnEnable()
    {
        /*_stateData = serializedObject.FindProperty("_stateData");
        _stateElementsProp = _stateData.FindPropertyRelative("StateElements");
        _globalFieldDictionary = _stateData.FindPropertyRelative("GlobalFieldDictionary");*/
        
        //_stateElementsProp = serializedObject.FindProperty("StateElements");
        //_globalFieldDictionaryProp = serializedObject.FindProperty("GlobalFieldDictionary");
//
        //var ret1 = (_stateElementsProp == null) ? "isNull" : "NotNull";
        //Debug.LogError($"stateElementProp {ret1}");
        //
        //var ret2 = (_globalFieldDictionaryProp == null) ? "isNull" : "NotNull";
        //Debug.LogError($"globalFieldDictionaryProp {ret2}");
        
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.BeginHorizontal();
        /*if (GUILayout.Button("Add IdleState"))
        {
            var data = (StateMachineData)target;
            data.StateElements.Add(new IdleState(null,null));
            EditorUtility.SetDirty(data);
            Debug.LogError(data.StateElements.Count);
        }
        if (GUILayout.Button("Add GlobalField"))
        {
            var data = (StateMachineData)target;
            data.GlobalFieldDictionary.Add(Random.Range(0,99999999).ToString(), new FloatField() { Value = 3.0f });
            EditorUtility.SetDirty(data);
        }*/
        // 기본 필드 표시
        //EditorGUILayout.PropertyField(prop, new GUIContent("My Data"), true);
        EditorGUILayout.EndHorizontal();
        
        EditorGUILayout.Space();
        StateMachineData stateMachineData = (StateMachineData)target;
        
        EditorGUILayout.LabelField("상태 리스트", EditorStyles.boldLabel);
        List<StateElement> stateElements = stateMachineData.StateElements;
        if (stateElements != null && stateElements.Count != 0)
        {
            for (int i = 0; i < stateElements.Count; i++)
            {
                StateElement element = stateElements[i];
                EditorGUILayout.LabelField($"[{i}] Type: {element.GetType()}   Guid: {element.Guid}");

                var dataDictionary = element.TargetGuidConditionDictionary;
                if (dataDictionary != null && dataDictionary.Count != 0)
                {
                    foreach (var targetGuidCondition in element.TargetGuidConditionDictionary)
                    {
                        EditorGUILayout.LabelField($"\t목표 Guid:{targetGuidCondition.Key}, ", EditorStyles.label);

                        var conditionList = targetGuidCondition.Value;
                        if (conditionList != null && conditionList.Count != 0)
                        {
                            foreach (var conditionData in conditionList)
                            {
                                EditorGUILayout.LabelField($"\t\t비교타입: {conditionData.CompareType}", EditorStyles.label);
                            }
                        }
                        else
                        {
                            EditorGUILayout.LabelField("\t\t조건이 존재하지 않음", EditorStyles.label);
                        }
                    }
                }
                else
                {
                    EditorGUILayout.LabelField("\t목표가 존재하지 않음", EditorStyles.label);
                }
            }
        }
        else
        {
            EditorGUILayout.LabelField("상태가 존재하지 않음", EditorStyles.label);
        }
        
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("글로벌 필드 리스트", EditorStyles.boldLabel);
        var globalFields = stateMachineData.GlobalFields;
        if (globalFields != null && globalFields.Count != 0)
        {
            foreach (var globalField in globalFields)
            {
                var globalFieldType = globalField.GetType();
                var propertyInfo = globalFieldType.GetProperty("Value");
                var value = "";
                if(propertyInfo != null)
                    value = propertyInfo.GetValue(globalField).ToString();
                else
                    value = "Null";
                EditorGUILayout.LabelField($"Type: {globalFieldType}   Value: {value}   Guid: {globalField.Guid}");
            }
        }
        else
        {
            EditorGUILayout.LabelField("글로벌 필드가 존재하지 않음", EditorStyles.label);
        }
        /*SerializedDictionary<string, GlobalField> globalFieldDictionary = stateMachineData.GlobalFieldDictionary;
        if (globalFieldDictionary != null && globalFieldDictionary.Count != 0)
        {
            foreach (var globalField in globalFieldDictionary)
            {
                var globalFieldType = globalField.Value.GetType();
                var propertyInfo = globalFieldType.GetProperty("Value");
                var value = "";
                if(propertyInfo != null)
                    value = propertyInfo.GetValue(globalField.Value).ToString();
                else
                    value = "Null";
                EditorGUILayout.LabelField($"Type: {globalFieldType}   Value: {value}   Guid: {globalField.Key}");
            }
        }
        else
        {
            EditorGUILayout.LabelField("글로벌 필드가 존재하지 않음", EditorStyles.label);
        }*/

        serializedObject.ApplyModifiedProperties();
    }
}
