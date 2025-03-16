using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AreaBase))]
public class AreaBaseEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); // 기본 인스펙터 표시

        AreaBase script = (AreaBase)target;
        if (GUILayout.Button("SpawnPos 로드")) // 버튼 생성
            script.LoadSpawnPos();
    }
}
