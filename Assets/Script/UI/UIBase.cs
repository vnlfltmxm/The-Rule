using System;
using UnityEngine;

public class UIBase : MonoBehaviour
{
    private void Awake()
    {
        OnAwake();
    }

    protected virtual void OnAwake()
    {
        
    }
}