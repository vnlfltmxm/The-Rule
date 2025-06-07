using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class StateMachineData : ScriptableObject
{
    //각 상태 노드
    [field: SerializeField]
    public List<StateElement> StateElements { get; private set; } = 
        new List<StateElement>();
    
    //글로벌 필드
    [field: SerializeField]
    public SerializedDictionary<string, GlobalField> GlobalFieldDictionary { get; private set; } =
        new SerializedDictionary<string, GlobalField>();
}
