using System;
using System.Collections;
using System.Collections.Generic;
using LN;
using Script.Manager.Framework;
using Script.Pawn;
using UnityEngine;
using UnityEngine.AI;

public class TrackingState : State
{
    private Transform _player;
    private WaitForSeconds _waitTime;
    private Coroutine _detectCoroutine;
    private LayerMask _targetLayer = LayerMask.GetMask("Player", "Wall");
    
    private float _calculateTime;
    private float _currentTime;
    private float _maxTime = 2.0f;

    private bool _isDetect;
    
    protected float _viewDistance;
    protected float _viewAngle;

    protected Vector3 _offSet = new Vector3(0f, 1f, 1f);
    
    protected NavMeshAgent _agent;
    protected Rigidbody _rigidbody;
    
    public override void InitState(Pawn stateObject)
    {
        _waitTime = new WaitForSeconds(0.1f);
        _agent = _stateObject.GetComponent<NavMeshAgent>();
        _rigidbody = _stateObject.GetComponent<Rigidbody>();
    }

    public override void StateEnter()
    {
        _isDetect = true;

        SetNavMeshAgent(true, 3f);

        _detectCoroutine = _stateObject.StartCoroutine(IsDetect());
    }
    public override Type StateCheck()
    {
        if (_calculateTime >= _maxTime)
        {
            _isDetect = false;
            return typeof(LookState);
        }
        /*if(other.gameObject.layer == LayerMask.NameToLayer("SafeArea"))
        {
            _isDetect = false;
            return typeof(IdleState);
        }*/
        return null;
    }
    public override void StateUpdate()
    {
        if (_isDetect)
        {
            Vector3 playerPosition = _player.position;

            _agent.SetDestination(playerPosition);
        }
    }
    public override void StateFixedUpdate()
    {
    }
    public override void StateLateUpdate()
    {
    }
    public override void StateExit()
    {
        SetNavMeshAgent(false, 0f);

        if (_detectCoroutine != null)
        {
            _stateObject.StopCoroutine(_detectCoroutine);

            _detectCoroutine = null;
        }

        _currentTime = 0f;
    }
    public override void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Logger.Log("잡았다");
        }
    }

    #region Func
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
            if (DetectionUtils.CalculateAngle(_stateObject.transform, _player, _viewAngle) && 
                DetectionUtils.CalculateDistance(_stateObject.transform, _player, _viewDistance) && 
                DetectionUtils.RayCast(_stateObject.transform, _player, _viewDistance, _offSet, _targetLayer))
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
    }
    #endregion
    
}
