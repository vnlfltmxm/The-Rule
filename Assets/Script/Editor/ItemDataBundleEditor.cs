using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ItemDataBundle))]
public class ItemDataBundleEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); // 기본 인스펙터 표시

        ItemDataBundle script = (ItemDataBundle)target;
        if (GUILayout.Button("ItemData 로드")) // 버튼 생성
            script.LoadData();
    }
}
