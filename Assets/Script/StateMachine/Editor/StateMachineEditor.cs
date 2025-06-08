using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(StateMachine))]
public class StateMachineEditor : Editor
{
    private SerializedProperty _stateData;
    private void OnEnable()
    {
        _stateData = serializedObject.FindProperty("_stateMachineData");
        //_globalFieldDatasProp = _stateData.FindPropertyRelative("GlobalFieldDatas");
        //_stateElementDatasProp = _stateData.FindPropertyRelative("StateElementDatas");
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
            StateMachineWindow.OpenStateMachineWindow(comp, comp.gameObject);
        }

        EditorGUILayout.EndHorizontal();
        
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("상태 머신 데이터", EditorStyles.boldLabel);
        
        EditorGUILayout.PropertyField(_stateData, new GUIContent($"StateMachineData"), true);
        
        serializedObject.ApplyModifiedProperties();
    }
}
