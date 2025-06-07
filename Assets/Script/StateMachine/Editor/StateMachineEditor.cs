using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(StateMachine))]
public class StateMachineEditor : Editor
{
    private SerializedProperty _stateData;
    private SerializedProperty _globalFieldDatasProp;
    private SerializedProperty _stateElementDatasProp;
    private void OnEnable()
    {
        _stateData = serializedObject.FindProperty("_stateData");
        _globalFieldDatasProp = _stateData.FindPropertyRelative("GlobalFieldDatas");
        _stateElementDatasProp = _stateData.FindPropertyRelative("StateElementDatas");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        //SerializedProperty prop = serializedObject.FindProperty("myData");

        EditorGUILayout.BeginHorizontal();

        // 기본 필드 표시
        //EditorGUILayout.PropertyField(prop, new GUIContent("My Data"), true);

        // 버튼 표시
        if (GUILayout.Button("상태 머신 편집기 실행", GUILayout.ExpandWidth(true)))
        {
            // 에디터 창 열기
            StateMachine comp = (StateMachine)target;
            StateMachineWindow.OpenStateMachineWindow();
        }

        EditorGUILayout.EndHorizontal();
        
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("글로벌 필드", EditorStyles.boldLabel);
        if (_globalFieldDatasProp != null)
        {
            for (int i = 0; i < _globalFieldDatasProp.arraySize; i++)
            {
                SerializedProperty element = _globalFieldDatasProp.GetArrayElementAtIndex(i);
                EditorGUILayout.PropertyField(element, new GUIContent($"Field {i}"), true);
            }
        }
        else
        {
            EditorGUILayout.LabelField("    필드가 존재하지 않음", EditorStyles.label);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
