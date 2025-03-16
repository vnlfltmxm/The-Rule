using System;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Overlays;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogGraph : EditorWindow
{
    private DialogGraphView _graphView;
    private string _fileName = "New Narrative";
    
    [MenuItem("Graph/Dialog Graph")]
    public static void OpenDialogGraphWindow()
    {
        var window = GetWindow<DialogGraph>();
        window.titleContent = new GUIContent("Dialog Graph");
    }

    private void OnEnable()
    {
        ConstructGraphView();
        GenerateToolbar();
        GenerateMiniMap();
    }


    //그래프뷰 생성
    private void ConstructGraphView()
    {
        _graphView = new DialogGraphView()
        {
            name = "Dialog Graph"
        };
        
        _graphView.StretchToParentSize();
        rootVisualElement.Add(_graphView);
    }

    //툴바 생성
    private void GenerateToolbar()
    {
        var toolbar = new Toolbar();

        //파일 이름 지정 텍스트필드
        var fileNameTextField = new TextField("파일 이름:");
        fileNameTextField.SetValueWithoutNotify(_fileName);
        fileNameTextField.MarkDirtyRepaint();
        fileNameTextField.RegisterValueChangedCallback(evt => _fileName = evt.newValue);
        toolbar.Add(fileNameTextField);

        toolbar.Add(new Button(()=>RequestDataOperation(true)){text = "데이터 저장"});
        toolbar.Add(new Button(()=>RequestDataOperation(false)){text = "데이터 불러오기"});
        
        //신규노드 생성 버튼
        var nodeCreateButton = new Button(() => { _graphView.CreateNode("Dialog Node"); });
        nodeCreateButton.text = "신규 노드 생성";
        toolbar.Add(nodeCreateButton);
        
        rootVisualElement.Add(toolbar);
    }
    
    private void GenerateMiniMap()
    {
        var miniMap = new MiniMap { anchored = true };
        miniMap.SetPosition(new Rect(10, 30, 200, 140));
        _graphView.Add(miniMap);
    }

    private void RequestDataOperation(bool isSave)
    {
        if (string.IsNullOrEmpty(_fileName))
        {
            EditorUtility.DisplayDialog("유효하지 않은 파일 이름 입니다", "유효한 파일 이름을 입력해주세요", "OK");
            return;
        }

        var saveUtility = GraphSaveUtility.GetInstance(_graphView);
        if(isSave)
            saveUtility.SaveGraph(_fileName);
        else
            saveUtility.LoadGraph(_fileName);
    }

    private void OnDisable()
    {
        rootVisualElement.Remove(_graphView);
    }
}
