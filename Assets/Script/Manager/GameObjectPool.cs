using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class GameObjectPool<T> where T : MonoBehaviour
{
    private Transform _poolParent;
    private Func<T> _poolFactory;
    private Stack<T> _pool = new Stack<T>();
    
    public GameObjectPool(Func<T> factoryMethod)
    {
        _poolParent = HierarchyManager.GetParent(typeof(T).Name);
        _poolFactory = factoryMethod;
    }
    public T GetObject()
    {
        return (_pool.Count > 0) ? _pool.Pop() : _poolFactory.Invoke();
    }
    public void ReturnObject(T obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(_poolParent);
        _pool.Push(obj);
    }
    
    /*private Dictionary<Type, Transform> _poolParents = new Dictionary<Type, Transform>();
    private Dictionary<Type, Stack<MonoBehaviour>> _pool = new Dictionary<Type, Stack<MonoBehaviour>>();
    private Dictionary<Type, Func<MonoBehaviour>> _poolFactory = new Dictionary<Type, Func<MonoBehaviour>>();*/

    
    /*protected override void Init()
    {
        
    }
    
    private bool IsInitPool<T>()
    {
        bool isInit = (_pool.ContainsKey(typeof(T))) ? true : false;
        if (isInit == false)
        {
            Logger.LogWarning($"Type: {typeof(T)} 에 해당하는 Pool을 초기화하지 않았습니다, 기본 Pool Stack 생성");
            _pool.Add(typeof(T), new Stack<MonoBehaviour>());
        }
        
        return isInit;
    }
    private bool IsInitFactory<T>()
    {
        bool isInit = (_poolFactory.ContainsKey(typeof(T))) ? true : false;
        if(isInit == false)
            Logger.LogError($"Type: {typeof(T)} 에 해당하는 PoolFactory를 초기화하지 않았습니다");
        return isInit;
    }

    public void InitPoolFactory<T>(Func<MonoBehaviour> factoryFunction) where T : MonoBehaviour
    {
        _poolFactory[typeof(T)] = factoryFunction;
    }
    public void InitPool<T>(int count) where T : MonoBehaviour
    {
        for (int i = 0; i < count; i++)
        {
            T obj = (T)_poolFactory[typeof(T)].Invoke();
            ReturnObject<T>(obj);
        }
    }
    public T GetObject<T>() where T : MonoBehaviour
    {
        IsInitPool<T>();
        IsInitFactory<T>();
        
        var pool = _pool[typeof(T)];
        if (pool.Count > 0)
        {
            MonoBehaviour obj = pool.Pop();
            return (T)obj;
        }
        else
        {
            MonoBehaviour obj = _poolFactory[typeof(T)].Invoke();
            return (T)obj;
        }
    }
    public void ReturnObject<T>(T obj) where T : MonoBehaviour
    {
        obj.transform.SetParent(GetPoolParent<T>());
        obj.gameObject.SetActive(false);
        _pool[typeof(T)].Push(obj);
    }
    
    private Transform GetPoolParent<T>() where T : MonoBehaviour
    {
        Type type = typeof(T);
        if (_poolParents.ContainsKey(type) == false)
        {
            Transform poolParent = new GameObject(type.ToString()).transform;
            poolParent.SetParent(this.transform);
            _poolParents[type] = poolParent;
        }
        
        return _poolParents[type];
    }*/
}