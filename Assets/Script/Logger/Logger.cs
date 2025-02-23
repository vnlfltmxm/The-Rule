using Unity.VisualScripting;
using UnityEngine;

public static class Logger
{
    [System.Diagnostics.Conditional("Dev_Version")]
    public static void Log(object message, Color color = default)
    {
        //if(color == default)
        //    color = Color.white;
        //Debug.Log($"<color=#{color.ToHexString()}>{message}</color>");
    }
    
    [System.Diagnostics.Conditional("Dev_Version")]
    public static void LogWarning(object message, Color color = default)
    {
        if(color == default)
            color = Color.white;
        Debug.LogWarning($"<color=#{color.ToHexString()}>{message}</color>");
    }
    
    [System.Diagnostics.Conditional("Dev_Version")]
    public static void LogError(object message, Color color = default)
    {
        if(color == default)
            color = Color.white;
        Debug.LogError($"<color=#{color.ToHexString()}>{message}</color>");
    }
    
    [System.Diagnostics.Conditional("Dev_Version")]
    public static void LogException(System.Exception exception)
    {
        Debug.LogException(exception);
    }
    
    [System.Diagnostics.Conditional("Dev_Version")]
    public static void Assert(bool condition)
    {
        Debug.Assert(condition);
    }
}
