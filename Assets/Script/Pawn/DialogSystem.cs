using System.Linq;
using UnityEngine;

public class DialogSystem : MonoBehaviour
{
    [SerializeField] private DialogContainer _dialogContainer;

    public void ActiveDialog()
    {
        DialogNodeData nodeData = _dialogContainer.EntryPointNode;
        UIManager.Instance.GetUI<DialogUI>().ShowText(transform, nodeData.DialogText);
    }
    public void InactiveDialog()
    {
        
    }
}
