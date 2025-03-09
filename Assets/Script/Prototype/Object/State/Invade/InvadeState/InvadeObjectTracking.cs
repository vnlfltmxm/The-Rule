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
    private LayerMask _targetLayer = LayerMask.GetMask("Player", "Wall");
    
    private float _calculateTime;
    private float _currentTime;
    private float _maxTime = 2.0f;
    private bool _isDetect;

    public void SetPlayer(Transform player)
    {
        _player = player;
    }

    public void StateEnter()
    {
        _isDetect = true;

        SetNavMeshAgent(true, 3f);

        if (!_invadeObject.SystemTracking)
        {
            _detectCoroutine = _invadeObject.StartCoroutine(IsDetect());
        }
    }

    public void StateUpdate()
    {
        if (_isDetect)
        {
            _agent.SetDestination(_player.position);
        }
    }

    public void StateExit()
    {
        SetNavMeshAgent(false, 0f);

        if (_detectCoroutine != null)
        {
            _invadeObject.StopCoroutine(_detectCoroutine);

            _detectCoroutine = null;
        }

        _currentTime = 0f;
    }
    
    public void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.layer)
        {
            case int layer when layer == LayerMask.NameToLayer("Player"):
                Debug.Log("잡았다");
                if (_invadeObject.SystemTracking)
                {
                    EnemyManager.Instance.StationWorkerCatchPlayer(_invadeObject);
                }
                break;
            case int layer when layer == LayerMask.NameToLayer("SafeArea"):
                if (!_invadeObject.SystemTracking)
                {
                    StopTracking();
                }
                break;
        }
    }

    public void StopTracking()
    {
        _isDetect = false;

        ChangeState(InvadeState.Idle);
    }

    private void ChangeState(InvadeState state)
    {
        _agent.SetDestination(_invadeObject.transform.position);

        _state.ChangeObjectState(state);
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

            if (calculate && RayCast())
            {
                _isDetect = true;

                _currentTime = Time.time;
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
        _calculateTime = Time.time - _currentTime;
        
        if (_calculateTime >= _maxTime)
        {
            _isDetect = false;

            ChangeState(InvadeState.Look);
        }
    }

    private bool RayCast()
    {
        _rayDirection = (_player.position - _invadeObject.transform.position).normalized;

        _rayOrigin = _invadeObject.transform.position + _invadeObject.transform.TransformDirection(_offSet);

        Debug.DrawRay(_rayOrigin, _rayDirection * (_viewDistance - _offSet.z), Color.red);

        if (Physics.Raycast(_rayOrigin, _rayDirection, out _hit, _viewDistance - _offSet.z, _targetLayer,QueryTriggerInteraction.Ignore))
        {
            if (_hit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                return true;
            }
        }

        return false;
    }
}
