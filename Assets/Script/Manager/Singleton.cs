using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = (T)FindFirstObjectByType(typeof(T));

                if(_instance == null)
                {
                    var sigletonObject = new GameObject();

                    _instance = sigletonObject.AddComponent<T>();
                }
            }

            return _instance;
        }
    }
}
