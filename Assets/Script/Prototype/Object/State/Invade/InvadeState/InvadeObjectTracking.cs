using System.Collections;
using UnityEngine;

public class InvadeObjectTracking : InvadeObjectMovement, IObjectState<InvadeObjectTracking>, ISetPlayer
{
    public InvadeObjectTracking(InvadeObject invadeObject) : base(invadeObject)
    {
        _waitTime = new WaitForSeconds(0.1f);
    }

    private Transform _player;
    private WaitForSeconds _waitTime;
    private Coroutine _detectCoroutine;
    
    private float _currentTime;
    private float _maxTime = 1.0f;

    private bool _isDetect;

    public void SetPlayer(Transform player)
    {
        _player = player;
    }

    public void StateEnter()
    {
        _isDetect = true;

        SetNavMeshAgent(true, 1f);

        if(_detectCoroutine != null)
        {
            _invadeObject.StopCoroutine(_detectCoroutine);

            _detectCoroutine = null;
        }

        _detectCoroutine = _invadeObject.StartCoroutine(IsDetect());
    }

    public void StateUpdate()
    {
        if (_isDetect)
        {
            Vector3 playerPosition = _player.position;

            _agent.SetDestination(playerPosition);
        }
        else
        {
            _state.ChangeObjectState(InvadeState.Look);
        }
    }

    public void StateExit()
    {
        SetNavMeshAgent(false, 0f);

        _detectCoroutine = null;

        _currentTime = 0f;
    }

    private void SetNavMeshAgent(bool isEnable, float stoppingDistance)
    {
        _rigidbody.isKinematic = isEnable;

        _agent.enabled = isEnable;

        _agent.stoppingDistance = stoppingDistance;
    }

    private IEnumerator IsDetect()
    {
        while (_isDetect)
        {
            bool calculate = CalculateAngle(_player, _invadeObject.transform) 
                && CalculateDistance(_player, _invadeObject.transform);

            if (calculate)
            {
                if (RayCast())
                {
                    _isDetect = true;

                    _currentTime = 0f;
                }
                else
                {
                    Timer();
                }
            }
            else
            {
                Timer();
            }

            yield return _waitTime;
        }
    }

    private void Timer()
    {
        _currentTime += Time.deltaTime;

        if (_currentTime > _maxTime)
        {
            _isDetect = false;
        }
    }

    private bool RayCast()
    {
        _rayDirection = (_player.position - _invadeObject.transform.position).normalized;

        _rayOrigin = _invadeObject.transform.position + _invadeObject.transform.TransformDirection(_offSet);

        Debug.DrawRay(_rayOrigin, _rayDirection * (_viewDistance - 1f), Color.red);

        if (Physics.Raycast(_rayOrigin, _rayDirection, out _hit, _viewDistance - 1f))
        {
            if (_hit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                return true;
            }
        }

        return false;
    }
}
