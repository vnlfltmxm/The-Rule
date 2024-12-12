using System.Collections;
using UnityEngine;

public class InvadeHead : MonoBehaviour
{
    [Header("Angle")]
    [SerializeField] private float _angle;
    [SerializeField] private float _rotationSpeed;
    public bool Completed { get; set; } = false;
    private bool _isRotating;

    private Quaternion _startRotation;
    private Coroutine _rotationCoroutine;

    private void Awake()
    {
        _startRotation = transform.localRotation;
    }

    public void StartHeadRotation()
    {
        if (!_isRotating)
        {
            _rotationCoroutine = StartCoroutine(HeadRotation());
        }
    }

    public void StopHeadRotation()
    {
        if(_rotationCoroutine != null)
        {
            StopCoroutine( _rotationCoroutine);

            _rotationCoroutine = null;
        }

        _isRotating = false;

        transform.localRotation = Quaternion.identity;
    }

    public IEnumerator HeadRotation()
    {
        _isRotating = true;

        Quaternion targetRotation;

        targetRotation = Quaternion.AngleAxis(_angle / 2, Vector3.up) * _startRotation;
        yield return StartCoroutine(RotationCoroutine(targetRotation));

        targetRotation = Quaternion.AngleAxis(-_angle / 2, Vector3.up) * _startRotation;
        yield return StartCoroutine(RotationCoroutine(targetRotation));

        targetRotation = _startRotation;
        yield return StartCoroutine(RotationCoroutine(targetRotation));

        _isRotating = false;
        Completed = true;
    }

    private IEnumerator RotationCoroutine(Quaternion targetRotation)
    {
        while(Quaternion.Angle(transform.localRotation, targetRotation) > 0.1f)
        {
            if (!_isRotating)
            {
                yield break;
            }

            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation,
                _rotationSpeed * Time.deltaTime);

            yield return null;
        }

        transform.localRotation = targetRotation;
    }

}
