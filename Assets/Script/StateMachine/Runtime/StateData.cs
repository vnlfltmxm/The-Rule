using System.Collections.Generic;
using UnityEngine;

public class StateData : ScriptableObject
{
    [SerializeField] public List<GlobalField> GlobalFieldDatas;
    [SerializeField] public List<StateElementData> StateElementDatas;
}
