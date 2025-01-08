using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    private void Awake()
    {
        gameObject.AddComponent<TestPlayerInputSystem>();
    }

    private void Start()
    {
        Cursor.visible = false;
    }
}
