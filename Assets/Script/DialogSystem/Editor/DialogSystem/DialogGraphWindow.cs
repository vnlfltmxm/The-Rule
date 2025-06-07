using System;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Overlays;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogGraphWindow : EditorWindow
{
    private DialogGraphView _graphView;
    private string _fileName = "New Narrative";

    private MiniMap _miniMap;
    private Blackboard _blackBoard;
    
    [MenuItem("Graph/Dialog Graph")]
    public static void OpenDialogGraphWindow()
    {
        var window = GetWindow<DialogGraphWindow>();
        window.titleContent = new GUIContent("Dialog Graph");
    }

    private void OnEnable()
    {
        ConstructGraphView();
        GenerateToolbar();
        GenerateMiniMap();
        GenerateBlackBoard();
    }

    //그래프뷰 생성
    private void ConstructGraphView()
    {
        _graphView = new DialogGraphView(this)
        {
            name = "Dialog Graph"
        };
        
        _graphView.StretchToParentSize();
        rootVisualElement.Add(_graphView);
    }

    #region Toolbar
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
        
        /*//신규노드 생성 버튼
        var nodeCreateButton = new Button(() => { _graphView.CreateNode("Dialog Node"); });
        nodeCreateButton.text = "신규 노드 생성";
        toolbar.Add(nodeCreateButton);*/
        
        rootVisualElement.Add(toolbar);
    }
    #endregion

    #region MiniMap
    //미니맵 생성
    private void GenerateMiniMap()
    {
        _miniMap = new MiniMap { anchored = true };
        _miniMap.RegisterCallback<ContextualMenuPopulateEvent>(evt =>
        {
            evt.menu.ClearItems(); // 메뉴 항목 전부 제거
            evt.StopPropagation(); // 이벤트 전파 막기 (선택사항)
        });
        
        _graphView.Add(_miniMap);
        _graphView.RegisterCallback<GeometryChangedEvent>(evt =>
        {
            Debug.Log("GraphView 크기 변경됨!");
            UpdateMiniMapPosition();
        });
        
        UpdateMiniMapPosition();
    }

    private void UpdateMiniMapPosition()
    {
        float miniMapWidth = 200f;
        float miniMapHeight = 140f;
        float miniMapTopOffset = 50f;
        float miniMapRightOffset = 10f;
        
        Vector2 topRight = new Vector2(position.width - miniMapWidth - miniMapRightOffset, miniMapTopOffset);
        Vector2 localPos = _graphView.contentViewContainer.WorldToLocal(topRight);
        _miniMap.SetPosition(new Rect(localPos.x, localPos.y, miniMapWidth, miniMapHeight));
    }
    #endregion
    
    #region BlackBoard
    private void GenerateBlackBoard()
    {
        _blackBoard = new Blackboard(_graphView);
        _blackBoard.Add(new BlackboardSection{title = "Exposed Properties"});
        _blackBoard.addItemRequested = blackboard => { _graphView.AddPropertyToBlackBoard(new ExposedProperty()); };
        _blackBoard.editTextRequested = (blackboard, element, newValue) =>
        {
            var oldPropertyName = ((BlackboardField)element).text;
            if (_graphView.ExposedProperties.Any(x => x.PropertyName == newValue))
            {
                EditorUtility.DisplayDialog("Error", "해당 프로퍼티 이름은 사용중입니다.","OK");
                return;
            }

            var propertyIndex = _graphView.ExposedProperties.FindIndex(x => x.PropertyName == oldPropertyName);
            _graphView.ExposedProperties[propertyIndex].PropertyName = newValue;
            ((BlackboardField)element).text = newValue;
        };
        
        _graphView.Blackboard = _blackBoard;
        _graphView.Add(_blackBoard);
        
        UpdateBlackBoardPosition();
    }
    
    private void UpdateBlackBoardPosition()
    {
        float blackBoardWidth = 200f;
        float blackBoardHeight = 300f;
        float blackBoardTopOffset = 30f;
        float blackBoardLeftOffset = 10f;
        
        _blackBoard.SetPosition(new Rect(blackBoardLeftOffset, blackBoardTopOffset, blackBoardWidth, blackBoardHeight));
    }
    #endregion
    
    
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
