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
    
    private GameObject _playerCamera; // 플레이어 카메라
    
    private InteractiveProp _currentDetectedProp;
    public InteractiveProp CurrentDetectedProp => _currentDetectedProp;
    
    public bool _hasDetected = false;
    public bool HasDetected => _hasDetected;

    //UI활성화/비활성화 이벤트
    public Action OnDetectedEvent;
    public Action OnDetectionLostEvent;

    public void Init(Player player)
    {
        _playerCamera = player.VirtualCamera;
    }
    
    void Update()
    {
        DetectProp();
    }

    private void DetectProp()
    {
        Ray ray = new Ray(_playerCamera.transform.position, _playerCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _detectionRange, _detectionLayer))
        {
            GameObject detectedProp = hit.collider.gameObject;

            /*if (_hasDetected == false)
            {
                _currentDetectedProp = detectedProp.GetComponent<InteractiveProp>();
                OnDetectedEvent?.Invoke();
            }
            else if (_currentDetectedProp != null && _currentDetectedProp.gameObject != detectedProp)
            {
                _hasDetected = true;
                _currentDetectedProp = detectedProp.GetComponent<InteractiveProp>();
                OnDetectedEvent?.Invoke();
            }
            */
            
            if (_currentDetectedProp?.gameObject != detectedProp)
            {
                _hasDetected = true;
                _currentDetectedProp = detectedProp.GetComponent<InteractiveProp>();
                OnDetectedEvent?.Invoke();
            }
        }
        else
        {
            // 감지된 아이템이 없으면 초기화
            if (HasDetected == true)
            {
                _hasDetected = false;
                _currentDetectedProp = null;
                OnDetectionLostEvent?.Invoke();
            }
        }
    }
}
