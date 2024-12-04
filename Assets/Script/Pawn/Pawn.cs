using System;
using UnityEngine;

public abstract class Pawn : MonoBehaviour
{
    private void Start()
    {
        Init();
    }

    protected abstract void Init();
}
