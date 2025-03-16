using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class GraphSaveUtility
{
    private DialogGraphView _targetGraphView;
    private DialogContainer _containerCache;
    private readonly string _saveDirectoryPath = $"Assets/Resources/Data/DialogData";

    private List<Edge> Edges => _targetGraphView.edges.ToList();
    private List<DialogNode> Nodes => _targetGraphView.nodes.ToList().Cast<DialogNode>().ToList();
    
    public static GraphSaveUtility GetInstance(DialogGraphView targetGraphView)
    {
        return new GraphSaveUtility
        {
            _targetGraphView = targetGraphView,
        };
    }

    public void SaveGraph(string fileName)
    {
        //TODO
        //연결된 곳이 없으면 저장하지 않음
        if (!Edges.Any()) return;

        var dialogContainer = ScriptableObject.CreateInstance<DialogContainer>();
        var connectedPorts = Edges.Where(x => x.input.node != null).ToArray();
        
        //노드간 연결 저장
        foreach (var connectedPort in connectedPorts)
        {
            var outputNode = connectedPort.output.node as DialogNode;
            var inputNode = connectedPort.input.node as DialogNode;
            
            dialogContainer.NodeLinks.Add(new NodeLinkData()
            {
                BaseNodeGuid = outputNode.GUID,
                PortName = connectedPort.output.portName,
                TargetNodeGuid = inputNode.GUID
            });
        }

        //노드 데이터 저장
        foreach (var dialogNode in Nodes.Where(node => !node.EntryPoint))
        {
            dialogContainer.DialogNodeData.Add(new DialogNodeData
            {
                Guid = dialogNode.GUID,
                DialogText = dialogNode.DialogText,
                Position = dialogNode.GetPosition().position,
            });
        }

        //폴더 생성
        string[] folders = _saveDirectoryPath.Split('/');
        string currentPath = folders[0];
        for (int i = 1; i < folders.Length; i++)
        {
            string nextFolder = $"{currentPath}/{folders[i]}";
            if (!AssetDatabase.IsValidFolder(nextFolder))
                AssetDatabase.CreateFolder(currentPath, folders[i]);
            currentPath = nextFolder;
        }
        
        //if (!AssetDatabase.IsValidFolder($"Assets/Resources"))
        //    AssetDatabase.CreateFolder("Assets", "Resources");
        
        AssetDatabase.CreateAsset(dialogContainer,$"{_saveDirectoryPath}/{fileName}.asset");
        AssetDatabase.SaveAssets();
    }
    public void LoadGraph(string fileName)
    {
        _containerCache = AssetDatabase.LoadAssetAtPath<DialogContainer>($"{_saveDirectoryPath}/{fileName}.asset");
        if (_containerCache == null)
        {
            EditorUtility.DisplayDialog("파일을 찾지 못했습니다", $"파일 이름: {fileName} 에 해당하는 파일을 찾지 못했습니다", "OK");
            return;
        }

        ClearGraph();
        CreateNodes();
        ConnectNodes();
    }

    private void ClearGraph()
    {
        Nodes.Find(x => x.EntryPoint).GUID = _containerCache.NodeLinks[0].BaseNodeGuid;
        foreach (var node in Nodes)
        {
            if(node.EntryPoint) continue;
            
            //node에 연결된 Edge를 삭제한다
            Edges.Where(x=>x.input.node==node).ToList().ForEach(edge => _targetGraphView.RemoveElement(edge));
            
            //node 제거
            _targetGraphView.RemoveElement(node);
        }
    }
    private void CreateNodes()
    {
        foreach (var nodeData in _containerCache.DialogNodeData)
        {
            var tempNode = _targetGraphView.CreateDialogNode(nodeData.DialogText);
            tempNode.GUID = nodeData.Guid;
            _targetGraphView.AddElement(tempNode);

            var nodePorts = _containerCache.NodeLinks.Where(x => x.BaseNodeGuid == nodeData.Guid).ToList();
            nodePorts.ForEach(x => _targetGraphView.AddChoicePort(tempNode, x.PortName));
        }
    }
    private void ConnectNodes()
    {
        for (int i = 0; i < Nodes.Count; i++)
        {
            var connections = _containerCache.NodeLinks.Where(x=>x.BaseNodeGuid == Nodes[i].GUID).ToList();
            for (int j = 0; j < connections.Count; j++)
            {
                var targetNodeGuid = connections[j].TargetNodeGuid;
                var targetNode = Nodes.First(x => x.GUID == targetNodeGuid);
                LinkNodes(Nodes[i].outputContainer[j].Q<Port>(),(Port) targetNode.inputContainer[0]);

                targetNode.SetPosition(new Rect(
                    _containerCache.DialogNodeData.First(x => x.Guid == targetNodeGuid).Position,
                    _targetGraphView.DefaultNodeSize
                ));
            }
        }
    }

    private void LinkNodes(Port output, Port input)
    {
        var tempEdge = new Edge
        {
            output = output,
            input = input
        };
        tempEdge.input.Connect(tempEdge);
        tempEdge.output.Connect(tempEdge);
        _targetGraphView.Add(tempEdge);
    }
}
