using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ParseDialog : MonoBehaviour
{
    [Header("EX) FilePath = Asset/TextData/...")]
    [Header("CSV_Path"), SerializeField] private string _filePath;
    [Header("SaveName"), SerializeField] private string _saveName;

    public void ParseDialogData()
    {
        var path = Path.Combine(Application.dataPath, _filePath);

        if (!File.Exists(path))
        {
            return;
        }

        SaveDialogData(path);
    }

    private void SaveDialogData(string path)
    {
        List<Dictionary<string, string>> dialogList = new List<Dictionary<string, string>>();

        using (StreamReader reader = new StreamReader(path))
        {
            
            string headerLine = reader.ReadLine();

            if(headerLine == null)
            {
                return;
            }

            string[] headers = headerLine.Split(',');

            while (!reader.EndOfStream)
            {
                string[] dataArray = reader.ReadLine().Split(',');

                Dictionary<string, string> rowDictionary = new Dictionary<string, string>();

                for(int i = 0; i < headers.Length; i++)
                {
                    rowDictionary[headers[i]] = dataArray.Length > i ? dataArray[i] : ""; //값이 없는 경우 빈 문자열 처리.
                }

                dialogList.Add(rowDictionary);
            }
        }

        string jsonData = JsonConvert.SerializeObject(dialogList, Formatting.Indented);

        string saveDirectory = Path.Combine(Application.streamingAssetsPath, "JsonDialog");

        if (!Directory.Exists(saveDirectory))
        {
            Directory.CreateDirectory(saveDirectory);
        }

        string savePath = Path.Combine(saveDirectory, $"{_saveName}.json");
        File.WriteAllText(savePath, jsonData);
    }
}
