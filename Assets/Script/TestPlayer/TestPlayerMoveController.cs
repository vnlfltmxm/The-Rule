using UnityEngine;

public class TestPlayerMoveController : MonoBehaviour
{
    private TestPlayerInputSystem _inputSystem;
    private Rigidbody _rigidbody;
    private Camera _playerCamera;

    [Header("CameraObject")]
    [SerializeField] private GameObject _cameraObject;

    private float _targetSpeed;
    private float _runSpeed = 7f;
    private float _speedOffset = 0.1f;
    private float _currentSpeed;
    private float _speedChangeValue = 100.0f;
    private float _currentHorizontalSpeed;

    [Header("Rotation")]
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _maxVerticalAngle;

    private float _verticalRotation;
    private float _horizontalRotation;

    private void Start()
    {
        _inputSystem = GetComponent<TestPlayerInputSystem>();
        _rigidbody = GetComponent<Rigidbody>();
        _playerCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Update()
    {
        Rotation();
    }

    private void Rotation()
    {
        var mouseDelta = _inputSystem.Delta;
        VerticalRotation(mouseDelta);
        HorizontalRotation(mouseDelta);
    }

    private void HorizontalRotation(Vector2 mouseDelta)
    {
        _horizontalRotation = mouseDelta.x * _rotationSpeed * Time.deltaTime;

        transform.Rotate(0f, _horizontalRotation, 0f, Space.Self);
    }

    private void VerticalRotation(Vector2 mouseDelta)
    {
        _verticalRotation -= mouseDelta.y * _rotationSpeed * Time.deltaTime;

        _verticalRotation = Mathf.Clamp(_verticalRotation, -_maxVerticalAngle, _maxVerticalAngle);

        _cameraObject.transform.localRotation = Quaternion.Euler(_verticalRotation, 0f, 0f);
    }

    private void Movement()
    {
        _targetSpeed = _runSpeed;

        if (_inputSystem.Input == Vector2.zero)
        {
            _targetSpeed = 0f;
        }

        _currentHorizontalSpeed = new Vector3(_rigidbody.linearVelocity.x, 0f, _rigidbody.linearVelocity.z).magnitude;

        bool isCorrection = _currentHorizontalSpeed < _targetSpeed - _speedOffset ||
            _currentHorizontalSpeed > _targetSpeed + _speedOffset;

        if (isCorrection)
        {
            _currentSpeed = Mathf.Lerp(_currentHorizontalSpeed, _targetSpeed, _speedChangeValue * Time.fixedDeltaTime);

            _currentSpeed = Mathf.Round(_currentSpeed * 1000f) / 1000f;
        }
        else
        {
            _currentSpeed = _targetSpeed;
        }

        Vector3 inputDirection = new Vector3(_inputSystem.Input.x, 0f, _inputSystem.Input.y).normalized;

        Vector3 moveDirection = Quaternion.Euler(0f,_playerCamera.transform.eulerAngles.y, 0f) * inputDirection;

        Vector3 moveVector = moveDirection.normalized * _currentSpeed;

        moveVector.y = _rigidbody.linearVelocity.y;

        _rigidbody.linearVelocity = moveVector;
    }


}
