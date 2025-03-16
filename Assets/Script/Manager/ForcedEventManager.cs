using System.Collections;
using Script.Pawn;
using Script.Pawn.Player;
using UnityEngine;

public class ForcedEventManager : SingletonMonoBehaviour<ForcedEventManager>
{
    //강제 이벤트 매니저
    
    public void PlayerLookAtEvent(Transform target)
    {
        
        Player.S_Player.SetActiveForcedEvent(true);
        StartCoroutine(LookAtRoutine(Player.S_Player, target));
    }

    private IEnumerator LookAtRoutine(Player player, Transform target)
    {
        while (true)
        {
            if (target != null)
            {
                Vector3 direction = target.position - player.transform.position; // 대상 오브젝트와 현재 오브젝트의 방향 벡터 계산
                direction.Normalize();
                Quaternion targetRotation = Quaternion.LookRotation(direction); // 방향 벡터를 기준으로 회전값 계산
                Vector3 targetEuler = targetRotation.eulerAngles;

                // 현재 회전 값과 목표 회전 값 간의 차이를 계산하여 부드럽게 회전
                while (Quaternion.Angle(player.transform.rotation, targetRotation) > 0.01f)
                {
                    player.PlayerMovementController.LookAt(targetEuler);
                    yield return null; // 한 프레임 대기
                }

                // 목표 회전 값에 도달했을 때
                player.transform.rotation = targetRotation;
                player.SetActiveForcedEvent(false);
                yield break;
            }
            else
            {
                player.SetActiveForcedEvent(false);
                yield break; // 대상이 없으면 코루틴 종료
            }
        }
    }
}
