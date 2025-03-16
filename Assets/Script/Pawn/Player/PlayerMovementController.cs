
using System;
using Script.Util;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.WSA;

namespace Script.Pawn.Player
{
    public class PlayerMovementController : MonoBehaviour
    {
	    
	    #region Field----------------------------------------------------------------------
	    private Rigidbody _rigidbody;
	    private PlayerInputHandler _inputHandler;
	    public Transform CinemachineCameraTarget;
	    public Transform HeadTransform;
	    public Transform BodyTransform;
	    
		#region 이동 관련----------------------------------------------------------------------
		[Header("이동 관련")]
		[Space]
		[Header("앉기 속도")]
		public float CrouchSpeed = 2.0f;
		[Header("이동 속도")]
	    public float MoveSpeed = 4.0f;
	    [Header("달리기 속도")]
	    public float SprintSpeed = 10.0f;
	    
	    
	    //지난 Fixed프레임 위치
	    private Vector3 lastFixedPosition;
	    //다음 Fixed프레임 위치
	    private Vector3 nextFixedPosition;
	    [Space]
	    #endregion ----------------------------------------------------------------------
	    #region 카메라 회전 관련 ----------------------------------------------------------------------
	    [Header("카메라 회전 관련")]
	    [Space]
	    [Header("카메라 회전 속도")]
	    public float RotationSpeed = 1.0f;
	    //카메라 상하 제한
	    public float TopClamp = 90.0f;
	    public float BottomClamp = -90.0f;
	    
	    //머리가 움직일 수 있는 최대 각도
	    private float _maxLookAngle = 70.0f;
	    
	    private float _deltaTimeMultiplier = 1.0f;
	    
	    private float _xLookAngle;//현재 상하 각도
	    private float _yLookAngle;//현재 좌우 각도
	    private float _yBodyAngle;//현재 신체 좌우 각도
	    
	    private const float _threshold = 0.01f;
	    #endregion ----------------------------------------------------------------------
	    #endregion ----------------------------------------------------------------------
	    
		#region Property ----------------------------------------------------------------------
		//현재 속도(달리기/걷기 속도)
		private float CurrentSpeed =>
			(_inputHandler.IsCrouch) ? 
				CrouchSpeed : (_inputHandler.IsSprint) ? SprintSpeed : MoveSpeed;
		
		//바닥의 노말벡터
		private Vector3 GroundNormalVector = Vector3.up;
		
		//플레이어기준 평면 방향벡터
		private Vector3 PlaneDirection => transform.forward * _inputHandler.MoveInputValue.y + transform.right * _inputHandler.MoveInputValue.x;
		
		//플레이어기준 바닥과 평행하는 방향벡터
		private Vector3 MoveDirection => Vector3.ProjectOnPlane(PlaneDirection, GroundNormalVector);
		
		//평면 Velocity (방향 곱하기 속도)
		private Vector3 MoveVelocity => MoveDirection * CurrentSpeed;
		
		//수직 Velocity (중력관성), 미완성, RigidBody의 Gravity를 사용하지 않고 독자적으로 중력 시물레이션을 적용해야 바닥과 평행하는 방향벡터의 영향을 받지않는 중력벡터를 얻을수 있음
		private float GravityVelocity => _rigidbody.linearVelocity.y;
		
		//최종 Velocity, 이걸 사용하라! 주저하지 말라!
		private Vector3 FinalVelocity => MoveVelocity;//중력 시물레이션이 없으므로 임시로 사용하도록 함. MoveVelocity + GravityVelocity;
		#endregion ----------------------------------------------------------------------
		
		public void Init(Player player)
		{
			_rigidbody = player.Rigidbody;
			_inputHandler = player.PlayerInputHandler;
			CinemachineCameraTarget = player.VirtualCamera;
			HeadTransform = player.HeadTransform;
			BodyTransform = player.BodyTransform;

			lastFixedPosition = player.transform.position;
			nextFixedPosition = player.transform.position;

			_inputHandler.OnCrouchEvent = Crouch;
			
			DebugManager.Instance.SetDebugData(DebugType.PlayerVelocity, FinalVelocity.ToString());
			DebugManager.Instance.SetDebugData(DebugType.PlayerPosition, transform.position.ToString());
			DebugManager.Instance.SetDebugData(DebugType.PlayerRotation, transform.rotation.ToString());
		}
		
        private void Update()
        {
	        OnUpdateCheakGround();
	        OnUpdateRotation();
	        OnUpdateMove();
	        
	        _rigidbody.linearVelocity = FinalVelocity;
	        transform.rotation = Quaternion.Euler(0, _yBodyAngle, 0);
	        HeadTransform.rotation = Quaternion.Euler(_xLookAngle, _yLookAngle, 0.0f);
	        CinemachineCameraTarget.rotation = Quaternion.Euler(_xLookAngle, _yLookAngle, 0.0f);
	        
	        DebugManager.Instance.SetDebugData(DebugType.PlayerVelocity, FinalVelocity.ToString());
	        DebugManager.Instance.SetDebugData(DebugType.PlayerPosition, transform.position.ToString());
	        DebugManager.Instance.SetDebugData(DebugType.PlayerRotation, transform.rotation.ToString());
        }

        public void LookAt(Vector3 targetEuler)
        {
	        float targetX = ClampAngle(targetEuler.x, -80f, 80f); // X축 클램프 적용
	        float targetY = Mathf.Repeat(targetEuler.y, 360f);

	        // 2️⃣ Lerp로 부드럽게 회전 적용
	        _xLookAngle = Mathf.LerpAngle(_xLookAngle, targetX, Time.deltaTime * RotationSpeed);
	        _yLookAngle = Mathf.LerpAngle(_yLookAngle, targetY, Time.deltaTime * RotationSpeed);

	        // 3️⃣ 회전 적용
	        transform.rotation = Quaternion.Euler(0, _yLookAngle, 0);
	        /*HeadTransform.rotation = Quaternion.Euler(_xLookAngle, _yLookAngle, 0.0f);
	        CinemachineCameraTarget.rotation = Quaternion.Euler(_xLookAngle, _yLookAngle, 0.0f);*/
        }
        

        #region 이동 관련 ----------------------------------------------------------------------
        private void OnUpdateMove()
        {
	        if (FinalVelocity != Vector3.zero)
		        _yBodyAngle = _yLookAngle;
        }
        #endregion ----------------------------------------------------------------------
        
        #region 회전 관련 ----------------------------------------------------------------------
        private void OnUpdateRotation()
        {
	        if (!(_inputHandler.LookInputValue.sqrMagnitude >= _threshold))
		        return;
	        
	        _xLookAngle += -_inputHandler.LookInputValue.y * RotationSpeed * _deltaTimeMultiplier;
	        _xLookAngle = ClampAngle(_xLookAngle, BottomClamp, TopClamp);
	        
	        _yLookAngle += _inputHandler.LookInputValue.x * RotationSpeed * _deltaTimeMultiplier;
	        _yLookAngle = Mathf.Repeat(_yLookAngle, 360f);
	        
	        OnUpdateBodyRotation();
        }
        private void OnUpdateBodyRotation()
        {
	        float diffAngle = -Mathf.DeltaAngle(_yLookAngle, _yBodyAngle);
	        
	        // 머리 각도가 제한을 넘어갔을 때 몸 회전 처리
	        if (Mathf.Abs(diffAngle) >= _maxLookAngle)
	        {
		        float bodyTurn = diffAngle + Mathf.Sign(diffAngle) * -_maxLookAngle;
		        _yBodyAngle = Mathf.Repeat(_yBodyAngle + bodyTurn, 360f);
	        }
        }
        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
	        if (lfAngle < -360f) lfAngle += 360f;
	        if (lfAngle > 360f) lfAngle -= 360f;
	        return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }
        #endregion ----------------------------------------------------------------------

        #region 지형 체크 ----------------------------------------------------------------------
        public LayerMask terrainLayer; // 체크할 레이어 (지형 레이어를 지정)
        public float rayDistance = 10f; // 레이캐스트 거리
        private void OnDrawGizmos()
        {
	        Gizmos.color = Color.cyan;
	        Gizmos.DrawLine(transform.position, transform.position - transform.up * rayDistance);
        }

        private void OnUpdateCheakGround()
        {
	        // Ray 시작점 (캐릭터의 현재 위치)
	        Vector3 origin = transform.position;

	        // Ray 방향 (캐릭터 아래 방향으로 발사)
	        Vector3 direction = Vector3.down;

	        // Raycast 결과 저장
	        if (Physics.Raycast(origin, direction, out RaycastHit hit, rayDistance, terrainLayer))
	        {
		        // 지형 히트 정보를 로그로 출력
		        //Debug.Log($"Hit at {hit.point}, Normal: {hit.normal}");
            
		        // 예: 히트된 위치에 디버그 구 표시
		        //Debug.DrawRay(hit.point, hit.normal, Color.red, 0.1f);

		        GroundNormalVector = hit.normal;
	        }
	        else
	        {
		        GroundNormalVector = Vector3.up;
	        }
        }
        #endregion ----------------------------------------------------------------------
        
        #region 기타 ----------------------------------------------------------------------
        private void Crouch(bool crouch)
        {
	        Logger.Log("Crouch!");
	        CinemachineCameraTarget.transform.localPosition = (crouch) ? Vector3.up : Vector3.up * 1.5f;
        }
        #endregion ----------------------------------------------------------------------
    }
}