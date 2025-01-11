using System;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData")]
public class ItemData : ScriptableObject
{
     [field: SerializeField] public int ID { get; private set; }
     [field: SerializeField] public string Name { get; private set; }
     [field: SerializeField][field: TextArea] public string Description { get; private set; }
     [field: SerializeField] public GameObject Prefab { get; private set; }
     [field: SerializeField] public Sprite Sprite { get; private set; }
}
