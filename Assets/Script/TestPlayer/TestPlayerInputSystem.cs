using UnityEngine;
using UnityEngine.InputSystem;

public class TestPlayerInputSystem : MonoBehaviour
{
    private MakeSound _makeSound;
    private PlayerInput _playerInput;

    private void Awake()
    {
        _makeSound = new MakeSound(this);
        _playerInput = GetComponent<PlayerInput>();
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

    private void OnInteraction(InputValue value)
    {
        var isPressed = value.isPressed;

        if (isPressed)
        {
            float sphereRadius = 1f;

            Vector3 spherePosition = transform.position + new Vector3(0f, 0.5f, 0.1f);

            LayerMask targetLayer = LayerMask.GetMask("Cafe");

            Collider[] colliders = Physics.OverlapSphere(spherePosition, sphereRadius, targetLayer);

            if (colliders.Length < 0)
            {
                return;
            }

            foreach (var collider in colliders)
            {
                IInteractionCafe cafe = collider.GetComponent<IInteractionCafe>();

                if (cafe != null)
                {
                    cafe.Interaction(gameObject);

                    return;
                }
            }
        }
    }

    public void PlayerLock(bool isLock)
    {
        _playerInput.enabled = !isLock;
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
