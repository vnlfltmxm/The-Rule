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

public class ObjectStateFactory<TObject, TState, TStateFactory> 
    where TObject : class 
    where TState : Enum 
    where TStateFactory : IStateFactory<TObject, TState>, new()
{
    private Dictionary<Type, object> _factorys = new Dictionary<Type, object>();

    public ObjectStateFactory()
    {
        _factorys[typeof(IStateFactory<TObject, TState>)] = new TStateFactory();
    }

    public Dictionary<TEnum, IObjectState> CreateState<TClass, TEnum>(TClass classType) where TClass : class where TEnum : Enum
    {
        var factoryType = typeof(IStateFactory<TClass, TEnum>);

        if (_factorys.ContainsKey(factoryType))
        {
            var factory = _factorys[factoryType] as IStateFactory<TClass, TEnum>;

            if(factory == null)
            {
                Logger.Log("StateDictionary is Null");
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

    public IObjectState CreateState<TClass, TEnum>(TClass classType, TEnum enumType)
        where TClass : class
        where TEnum : Enum
    {
        var factoryType = typeof(IStateFactory<TClass, TEnum>);

        if (_factorys.ContainsKey(factoryType))
        {
            var factory = _factorys[factoryType] as IStateFactory<TClass, TEnum>;

            if(factory == null)
            {
                Logger.Log("Factory Null");

                return null;
            }

            IObjectState state = factory.CreateState(classType, enumType);

            return state;
        }

        Logger.Log("Factory Value Null");
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
            case InvadeState.Trace:
                return new InvadeObjectTrace(classType);
            default:
                throw new ArgumentException();
        }
    }
}
public class MomMonsterStateFactory : IStateFactory<MomMonster, MomState>
{
    public IObjectState CreateState(MomMonster classType, MomState enumType)
    {
        switch (enumType)
        {
            case MomState.Idle:
                return new MomMonsterIdle(classType);
            case MomState.Tracking:
                return new MomMonsterTracking(classType);
            default:
                throw new ArgumentException();
        }
    }
}
public class ChildMonsterStateFactory : IStateFactory<ChildMonster, ChildState>
{
    public IObjectState CreateState(ChildMonster classType, ChildState enumType)
    {
        switch (enumType)
        {
            case ChildState.Idle:
                return new ChildMonsterIdle(classType);
            case ChildState.Cry:
                return new ChildMonsterCry(classType);
            case ChildState.Tracking:
                return new ChildMonsterTracking(classType);
            default:
                throw new ArgumentException();
        }
    }
}
#endregion
