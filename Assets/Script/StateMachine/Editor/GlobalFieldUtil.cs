using System;
using System.Collections.Generic;
using System.Linq;
using Script.Util;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public static class GlobalFieldUtil
{
    public static Type TypeAliasResolver(string typeName)
    {
        return typeName switch
        {
            "bool" => typeof(bool),
            "byte" => typeof(byte),
            "sbyte" => typeof(sbyte),
            "char" => typeof(char),
            "decimal" => typeof(decimal),
            "double" => typeof(double),
            "float" => typeof(float),
            "int" => typeof(int),
            "uint" => typeof(uint),
            "long" => typeof(long),
            "ulong" => typeof(ulong),
            "object" => typeof(object),
            "short" => typeof(short),
            "ushort" => typeof(ushort),
            "string" => typeof(string),
            "GameObject" => typeof(GameObject),
            _ => typeof(string)
        };
    }
    public static VisualElement CreateGlobalFieldForType(string typeName)
    {
        Type fieldType = TypeAliasResolver(typeName);
        if (fieldType == typeof(int))
            return new UnityEngine.UIElements.IntegerField();
        else if (fieldType == typeof(float))
            return new UnityEngine.UIElements.FloatField();
        else if (fieldType == typeof(bool))
            return new UnityEngine.UIElements.Toggle();
        else if (fieldType == typeof(string))
            return new UnityEngine.UIElements.TextField();
        else if (typeof(UnityEngine.Object).IsAssignableFrom(fieldType))
        {
            var objField = new UnityEditor.UIElements.ObjectField();
            objField.objectType = fieldType;
            return objField;
        }
        else
            return new TextField();
    }

    public static DropdownField CreateCompareDropdownFieldForType(ConditionData conditionData)
    {
        List<CompareType> compareTypes = conditionData.GetCompareTypes();
        if (compareTypes == null)
            return null;
        List<string> options = compareTypes.Select(ct => ct.ToString()).ToList();
        return new UnityEngine.UIElements.DropdownField(options, options.First());
    }

    public static VisualElement CreateTargetFieldForType(ConditionData conditionData)
    {
        if (conditionData is IntConditionData)
            return new UnityEngine.UIElements.IntegerField();
        else if (conditionData is FloatConditionData)
            return new UnityEngine.UIElements.FloatField();
        else if (conditionData is BooleanConditionData)
            return null;
        else if (conditionData is UnityObjectConditionData)
        {
            var objField = new UnityEditor.UIElements.ObjectField();
            objField.objectType = typeof(UnityEngine.Object);
            return objField;
        }

        return null;
    }
}
