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
    //private float _rotationVelocity;
    //private float _rotationChangeValue = 0.12f;
    //private float _targetRotation;
    private float _currentHorizontalSpeed;

    [Header("Rotation")]
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _maxVerticalAngle;
    [SerializeField] private float _maxHorizontalAngel;

    private float _currentBodyRotationY;
    private float _verticalRotation;
    private float _horizontalRotation;

    private void Start()
    {
        _inputSystem = GetComponent<TestPlayerInputSystem>();
        _rigidbody = GetComponent<Rigidbody>();
        _playerCamera = Camera.main;

        _currentBodyRotationY = transform.eulerAngles.y;
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

        float bodyRotation = mouseDelta.x * _rotationSpeed * Time.deltaTime;
        
        float angle = Vector3.Angle(transform.forward, _cameraObject.transform.forward);
        
        if(angle > _maxHorizontalAngel)
        {

        }
        else
        {
            _cameraObject.transform.Rotate(0f, bodyRotation, 0f, Space.Self);
        }


        //if(Mathf.Abs(angle) > _maxHorizontalAngel)
        //{
        //    float targetBodyRotation = _cameraObject.transform.eulerAngles.y;

        //    transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, targetBodyRotation, 0f), _rotationSpeed * Time.deltaTime);

        //    if(Mathf.Abs(Vector3.SignedAngle(transform.forward, _cameraObject.transform.forward, Vector3.up)) < 0.1f)
        //    {
        //        _cameraObject.transform.localRotation = Quaternion.identity;
        //    }
        //}
        //else
        //{
        //    _cameraObject.transform.Rotate(0f, bodyRotation, 0f, Space.Self);
        //}

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

        //if (inputDirection != Vector3.zero)
        //{
        //    if(inputDirection.z >= 0)
        //    {
        //        _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg
        //      + _playerCamera.transform.eulerAngles.y;

        //        float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity, _rotationChangeValue);

        //        transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);
        //    }
        //    else
        //    {
        //        _targetRotation = transform.eulerAngles.y;
        //    }
        //}

        //Vector3 moveDirection = inputDirection.z < 0 ? Quaternion.Euler(0f, _targetRotation, 0f) * Vector3.back
        //    : Quaternion.Euler(0f, _targetRotation, 0f) * Vector3.forward;

        Vector3 moveDirection = Quaternion.Euler(0f,_playerCamera.transform.eulerAngles.y, 0f) * inputDirection;

        Vector3 moveVector = moveDirection.normalized * _currentSpeed;

        moveVector.y = _rigidbody.linearVelocity.y;

        _rigidbody.linearVelocity = moveVector;
    }


}
