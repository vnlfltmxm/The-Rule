using UnityEngine;

public class CameraController : MonoBehaviour
{
    private TestPlayerInputSystem _input;
    private Transform _parentTransform;

    void Start()
    {
        _input = transform.parent.GetComponent<TestPlayerInputSystem>();
        _parentTransform = transform.parent.transform;
    }

    void Update()
    {
        
    }

    private void Rotation()
    {

    }
}
