using System;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewItem", menuName = "Data/Item")]
public class ItemData : ScriptableObject
{
     [field: SerializeField] public string ID { get; private set; }
     [field: SerializeField] public string Name { get; private set; }
     [field: SerializeField][field: TextArea] public string Description { get; private set; }
     [field: SerializeField] public Sprite Sprite { get; private set; }
}
