using System;
using Script.Pawn.Player;
using Script.Prop;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class RaycastPropDetector : MonoBehaviour
{
    [Header("레이캐스트 감지 거리")]
    [SerializeField] private float _detectionRange = 5f; // 레이캐스트 감지 거리
    [Header("감지할 레이어")]
    [SerializeField] private LayerMask _detectionLayer; // 감지할 레이어
    
    private Transform _playerCamera; // 플레이어 카메라
    
    private IInteractable _currentDetectedInteractable;
    public IInteractable CurrentDetectedInteractable => _currentDetectedInteractable;
    
    public bool _hasDetected = false;
    public bool HasDetected => _hasDetected;

    //UI활성화/비활성화 이벤트
    public Action OnDetectedEvent;
    public Action OnDetectionLostEvent;
    
    //임시 UI
    public GameObject InteractUI;

    public void Init(Player player)
    {
        _playerCamera = player.VirtualCamera;
        OnDetectedEvent = () => { InteractUI.SetActive(true); };
        OnDetectionLostEvent = () => { InteractUI.SetActive(false); };
    }
    
    void Update()
    {
        DetectProp();
    }

    private void DetectProp()
    {
        Ray ray = new Ray(_playerCamera.position, _playerCamera.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _detectionRange, _detectionLayer))
        {
            GameObject detectedProp = hit.collider.gameObject;

            if (detectedProp.TryGetComponent<IInteractable>(out IInteractable detectedInteractable))
            {
                // 감지된 아이템이 없으면 초기화
                if (detectedInteractable.IsInteractable == false)
                {
                    _hasDetected = false;
                    _currentDetectedInteractable = null;
                    OnDetectionLostEvent?.Invoke();
                }

                if (_currentDetectedInteractable != detectedInteractable)
                {
                    _hasDetected = true;
                    _currentDetectedInteractable = detectedProp.GetComponent<InteractiveProp>();
                    OnDetectedEvent?.Invoke();
                }
            }
        }
        else
        {
            // 감지된 아이템이 없으면 초기화
            if (HasDetected == true)
            {
                _hasDetected = false;
                _currentDetectedInteractable = null;
                OnDetectionLostEvent?.Invoke();
            }
        }
    }
}
