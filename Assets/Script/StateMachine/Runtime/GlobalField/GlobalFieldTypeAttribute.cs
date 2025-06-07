using System;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class GlobalFieldTypeAttribute : Attribute
{
    public string Name { get; }

    public GlobalFieldTypeAttribute(string name)
    {
        Name = name;
    }
}