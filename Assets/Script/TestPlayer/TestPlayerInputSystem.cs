using UnityEngine;
using UnityEngine.InputSystem;

public class TestPlayerInputSystem : MonoBehaviour
{
    public Vector2 Input { get; set; }
    public Vector2 Delta { get; set; }

    private void OnMove(InputValue value)
    {
        var moveVector = value.Get<Vector2>();

        SetVector2(moveVector);
    }

    public void SetVector2(Vector2 value)
    {
        Input = value;
    }

    private void OnDelta(InputValue value)
    {
        var mouseDelta = value.Get<Vector2>();

        SetDelta(mouseDelta);
    }

    public void SetDelta(Vector2 value)
    {
        Delta = value;
    }

}
