using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ParseDialog))]
public class DialogEditor : Editor
{
    private ParseDialog _parseDialog;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        _parseDialog = (ParseDialog)target;

        if (GUILayout.Button("Save"))
        {
            _parseDialog.ParseDialogData();
        }
    }
}
