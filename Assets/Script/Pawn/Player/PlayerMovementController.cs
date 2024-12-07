
using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.WSA;

namespace Script.Pawn.Player
{
	enum MoveType
	{
		Velocity,
		AddForce,
		MovePosition,
	}
    public class PlayerMovementController : MonoBehaviour
    {
	    
	    #region Field----------------------------------------------------------------------
	    private Rigidbody _rigidbody;
	    private PlayerInputHandler _inputHandler;
	    public GameObject CinemachineCameraTarget;
		#region 이동 관련----------------------------------------------------------------------
		[Header("이동 관련")]
		[Space]
		[SerializeField] private MoveType moveType;
		[SerializeField] private ForceMode forceMode;
		
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
	    
	    private float _deltaTimeMultiplier = 1.0f;
	    private float _cinemachineTargetPitch;
	    
	    private float _rotationVelocity;
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

			lastFixedPosition = player.transform.position;
			nextFixedPosition = player.transform.position;

			_inputHandler.OnCrouchEvent = Crouch;
		}
		
        private void Update()
        {
	        OnUpdateCheakGround();
	        switch (moveType)
	        {
		        case MoveType.Velocity:
			        MoveUseRigid_Velocity();
			        break;
		        case MoveType.AddForce:
			        MoveUseRigid_AddForce();
			        break;
		        case MoveType.MovePosition:
			        MoveUseRigid_MovePosition_And_MoveTowards();
			        break;
	        }
	        CameraRotation();
        }

        private void FixedUpdate()
        {
	        if (moveType == MoveType.MovePosition)
	        {
		        lastFixedPosition = nextFixedPosition;
		        nextFixedPosition += FinalVelocity * Time.fixedDeltaTime;
	        }
        }

        #region 이동 관련 ----------------------------------------------------------------------
        private void MoveUseRigid_Velocity()
        {
	        _rigidbody.linearVelocity = FinalVelocity;
	        //제일 간단하게 작성할 수 있는 이동로직, 정직하게 입력이 있을 때 움직이고 아닐 때는 멈춤
        }
        private void MoveUseRigid_AddForce()
        {
	        _rigidbody.AddForce(FinalVelocity, forceMode);
	        //Velocity로 인하여 미끄러짐 현상이 일어남, 감속 로직 추가로 작성해야함
        }
        private void MoveUseRigid_MovePosition_And_MoveTowards()
        {
	        Vector3 position = Vector3.MoveTowards(lastFixedPosition, nextFixedPosition, MoveSpeed * Time.deltaTime);
	        _rigidbody.MovePosition(position);
	        //물체를 뚫고 지나감, 물체충동처리는 추가로 작성해야함
        }
        #endregion ----------------------------------------------------------------------
        
        #region 회전 관련 ----------------------------------------------------------------------
        private void CameraRotation()
        {
	        // if there is an input
	        if (!(_inputHandler.LookInputValue.sqrMagnitude >= _threshold))
		        return;
	        
	        _cinemachineTargetPitch += -_inputHandler.LookInputValue.y * RotationSpeed * _deltaTimeMultiplier;
	        _rotationVelocity = _inputHandler.LookInputValue.x * RotationSpeed * _deltaTimeMultiplier;

	        // clamp our pitch rotation
	        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

	        // Update Cinemachine camera target pitch
	        CinemachineCameraTarget.transform.localRotation = Quaternion.Euler(_cinemachineTargetPitch, 0.0f, 0.0f);

	        // rotate the player left and right
	        transform.Rotate(Vector3.up * _rotationVelocity);
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