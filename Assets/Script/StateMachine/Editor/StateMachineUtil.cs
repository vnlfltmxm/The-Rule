using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class StateMachineUtil
{
    #region Data
    public static StateMachineData StateMachineData;
    #endregion
    
    #region EditorElement
    public static List<StateMachineNode> StateMachineNodes = new List<StateMachineNode>();
    public static List<ProgressLine> ProgressLines = new List<ProgressLine>();
    #endregion
    
    public static string GetPath(string assetName) => $"Assets/Resources/Data/StateData/{assetName}.asset";

    public static void LoadData(StateMachineData data)
    {
        StateMachineData = data;
        //StateMachineNodes
        //ProgressLines
    }

    public static void ClearData()
    {
        StateMachineNodes.Clear();
        ProgressLines.Clear();
    }
    
    public static void SaveData()
    {
        
    }

    public static void LoadStateMachineData(string assetName)
    {
        StateMachineData = AssetDatabase.LoadAssetAtPath<StateMachineData>(GetPath(assetName));
    }
    public static StateMachineData CreateStateMachineData(string assetName)
    {
        StateMachineData = ScriptableObject.CreateInstance<StateMachineData>();
        
        // 에셋으로 저장
        AssetDatabase.CreateAsset(StateMachineData, GetPath(assetName));
        AssetDatabase.SaveAssets();

        // 생성된 에셋을 선택
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = StateMachineData;
        return StateMachineData;
    }

    /*public static List<ProgressLine> FindProgressLines()
    {
        
    }*/
}