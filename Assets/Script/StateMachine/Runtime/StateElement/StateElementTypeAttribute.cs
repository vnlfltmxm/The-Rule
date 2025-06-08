using System;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class StateElementTypeAttribute : Attribute
{
    public Type StateType { get; }

    public StateElementTypeAttribute(Type type)
    {
        StateType = type;
    }
}