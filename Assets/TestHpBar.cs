using System;
using UnityEngine;
using UnityEngine.UI;

public class TestHpBar : MonoBehaviour
{
    [SerializeField] private Slider _hpBar;
    private static TestHpBar _instance;
    
    private void Awake()
    {
        _instance = this;
    }

    public static void UpdateHpBar(float ratio)
    {
        if(_instance != null)
            _instance._hpBar.value = ratio;
    }
}
