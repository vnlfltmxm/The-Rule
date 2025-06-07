using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialogContainer : ScriptableObject
{
    public DialogNodeData EntryPointNode;
    public List<NodeLinkData> NodeLinks = new List<NodeLinkData>();
    public List<DialogNodeData> DialogNodeData = new List<DialogNodeData>();
    public List<ExposedProperty> ExposedProperties = new List<ExposedProperty>();
}
