using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ItemData
{
     [field: SerializeField] public int ID { get; private set; }
     [field: SerializeField] public string Name { get; private set; }
     [field: SerializeField][field: TextArea] public string Description { get; private set; }
     [field: SerializeField] public Sprite Sprite { get; private set; }
}
