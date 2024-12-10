using System;
using System.Collections.Generic;
using UnityEngine;


public class EnemyFactory<TClass, TEnum>
    where TClass : class
    where TEnum : Enum
{
    public virtual TClass Create(TEnum enumType) { return null; }
}

#region StateFactory

public class ObjectStateFactory
{
    private Dictionary<Type, object> _factorys = new Dictionary<Type, object>();

    public ObjectStateFactory()
    {
        _factorys[typeof(IStateFactory<InvadeObject, InvadeState>)] = new InvadeObjectStateFactory();
    }

    public Dictionary<TEnum, IObjectState> CreateState<TClass, TEnum>(TClass classType)
        where TClass : class
        where TEnum : Enum
    {
        var factoryType = typeof(IStateFactory<TClass, TEnum>);

        if (_factorys.ContainsKey(factoryType))
        {
            var factory = _factorys[factoryType] as IStateFactory<TClass, TEnum>;

            if(factory == null)
            {
                Debug.Log("StateDictionary is Null");
                return new Dictionary<TEnum, IObjectState>();
            }

            Dictionary<TEnum, IObjectState> stateDictionary = new Dictionary<TEnum, IObjectState>();

            var enumValue = (TEnum[])Enum.GetValues(typeof(TEnum));

            foreach (TEnum stateType in enumValue)
            {
                IObjectState newState = factory.CreateState(classType, stateType);

                stateDictionary.Add(stateType, newState);
            }

            return stateDictionary;
        }

        return null;
    }
}
public interface IStateFactory<TClass, TEnum>
    where TClass : class
    where TEnum : Enum
{
    public IObjectState CreateState(TClass classType, TEnum enumType);
}

public class InvadeObjectStateFactory : IStateFactory<InvadeObject, InvadeState>
{
    public IObjectState CreateState(InvadeObject classType, InvadeState enumType)
    {
        switch (enumType)
        {
            case InvadeState.Idle:
                return new InvadeObjectIdle(classType);
            case InvadeState.Look:
                return new InvadeObjectLook(classType);
            case InvadeState.Patrol:
                return new InvadeObjectPatrol(classType);
            case InvadeState.Tracking:
                return new InvadeObjectTracking(classType);
            default:
                throw new ArgumentException();
        }
    }
}
#endregion
