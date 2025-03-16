using System.Linq;
using UnityEngine;

public class DialogSystem : MonoBehaviour
{
    [SerializeField] private DialogContainer _dialogContainer;

    public void ActiveDialog()
    {
        NodeLinkData linkData = _dialogContainer.NodeLinks.First();
        DialogNodeData nodeData = _dialogContainer.DialogNodeData.First(data => data.Guid == linkData.TargetNodeGuid);
        UIManager.Instance.GetUI<DialogUI>().ShowText(transform, nodeData.DialogText);
    }
    public void InactiveEndDialog()
    {
        
    }
}
