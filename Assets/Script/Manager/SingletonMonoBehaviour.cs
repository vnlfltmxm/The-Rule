using System;
using UnityEngine;

public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    { 
        get
        {
            if (_instance == null)
                InitInstance();

            return _instance;
        }
    }

    private static void InitInstance()
    {
        _instance = FindFirstObjectByType<T>();
    }
    
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
        }
        else if (_instance != this)
        {
            Logger.LogError($"싱글톤 인스턴스 중복 생성 Type: {typeof(T).Name}");
            Destroy(gameObject);
            return;
        }
        OnAwake();
    }
    
    protected virtual void OnAwake()
    {
        
    }
}
