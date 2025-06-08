using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class StateMachineData : ScriptableObject
{
    //각 상태 노드
    [field: SerializeReference]
    public List<StateElement> StateElements { get; private set; } = 
        new List<StateElement>();
    
    /*/// <summary>
    /// Key=글로벌 필드 Guid,
    /// Value=글로벌 필드
    /// </summary>
    [field: SerializeField]
    public SerializedDictionary<string, GlobalField> GlobalFieldDictionary { get; private set; } =
        new SerializedDictionary<string, GlobalField>();*/
    
    [field: SerializeField]
    public List<GlobalField> GlobalFields { get; private set; } =
        new List<GlobalField>();
}
