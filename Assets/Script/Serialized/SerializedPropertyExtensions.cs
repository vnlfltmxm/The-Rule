using UnityEditor;

public static class SerializedPropertyExtensions
{
    /// <summary>
    /// SerializedProperty를 원본 객체로 변환
    /// </summary>
    /// <typeparam name="T">원본 객체의 타입</typeparam>
    /// <param name="property">SerializedProperty</param>
    /// <returns>원본 객체</returns>
    public static T GetValue<T>(this SerializedProperty property)
    {
        object obj = property.serializedObject.targetObject;
        string[] path = property.propertyPath.Split('.');

        foreach (var segment in path)
        {
            var type = obj.GetType();
            var field = type.GetField(segment,
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Public |
                System.Reflection.BindingFlags.Instance);

            if (field == null)
                return default;

            obj = field.GetValue(obj);
        }

        return (T)obj;
    }
    
    /// <summary>
    /// SerializedProperty에 값을 설정
    /// </summary>
    /// <typeparam name="T">설정할 값의 타입</typeparam>
    /// <param name="property">SerializedProperty</param>
    /// <param name="value">새로운 값</param>
    public static void SetValue<T>(this SerializedProperty property, T value)
    {
        object obj = property.serializedObject.targetObject;
        string[] path = property.propertyPath.Split('.');

        for (int i = 0; i < path.Length - 1; i++)
        {
            var type = obj.GetType();
            var field = type.GetField(path[i], 
                System.Reflection.BindingFlags.NonPublic | 
                System.Reflection.BindingFlags.Public | 
                System.Reflection.BindingFlags.Instance);

            if (field == null)
                return;

            obj = field.GetValue(obj);
        }

        var targetType = obj.GetType();
        var targetField = targetType.GetField(path[^1], 
            System.Reflection.BindingFlags.NonPublic | 
            System.Reflection.BindingFlags.Public | 
            System.Reflection.BindingFlags.Instance);

        if (targetField == null)
            return;

        targetField.SetValue(obj, value);
        property.serializedObject.ApplyModifiedProperties();
    }
}