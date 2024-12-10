using UnityEngine;
using UnityEngine.InputSystem;

public class TestPlayerInputSystem : MonoBehaviour
{
    private MakeSound _makeSound;

    private void Awake()
    {
        _makeSound = new MakeSound(this);
    }

    public Vector2 Input { get; set; }
    public Vector2 Delta { get; set; }
    public bool IsRun { get; set; }

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

    private void OnRun(InputValue value)
    {
        var isPressed = value.isPressed;

        IsRun = isPressed;
        
        _makeSound.StartSound(isPressed, 20f);
    }

    private void OnDrawGizmos()
    {
        if (IsRun)
        {
            Gizmos.color =Color.red;

            Gizmos.DrawWireSphere(transform.position, 20f);
        }
    }

}
