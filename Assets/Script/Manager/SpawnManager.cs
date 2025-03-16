using System;
using System.Collections.Generic;
using Script.Pawn.Player;
using Script.Util;
using UnityEngine;

public class SpawnManager : SingletonMonoBehaviour<SpawnManager>
{
    private List<GameObject> _spawnedPawns = new List<GameObject>();

    public void SpawnPawn(SpawnData spawnData)//GameObject prefab, AreaType areaType = AreaType.Null)
    {
        Transform parent = HierarchyManager.Instance.GetParent(HierarchyParentType.PawnParent);
        Vector3 spawnPos = Vector3.zero;
        switch (spawnData.SpecialSpawnType)
        {
            //기본 Stage Area의 SpawnPos에 생성
            case SpecialSpawnType.SpawnPos:
                spawnPos = StageManager.Instance.GetRandomSpawnPos(spawnData.SpawnAreaTypes);
                break;
            case SpecialSpawnType.PlayerBack:
                spawnPos = GetPlacementPosition(Player.S_Player.transform, -Player.S_Player.transform.forward, 2f, LayerMask.NameToLayer("Wall"));
                break;
        }
        //spawnPos = StageManager.Instance.GetRandomSpawnPos(spawnData.SpawnAreaTypes);
        GameObject pawn = Instantiate(spawnData.MonsterPrefab, spawnPos, Quaternion.identity, parent);
        _spawnedPawns.Add(pawn);
        
        //스폰시 발생할 이벤트
        switch (spawnData.SpawnEventType)
        {
            case SpawnEventType.PlayerLookSpawned:
                ForcedEventManager.Instance.PlayerLookAtEvent(pawn.transform);
                break;
        }
    }
    public void RemovePawn(GameObject go)
    {
        _spawnedPawns.Remove(go);
        Destroy(go);
    }

    public bool HasObjectByName(string objectName)
    {
        foreach (var spawnedObject in _spawnedPawns)
        {
            if (spawnedObject.name.Contains(objectName))
                return true;
        }
        return false;
    }
    
    
    // 오브젝트를 배치할 위치를 계산하는 함수
    public Vector3 GetPlacementPosition(Transform origin, Vector3 direction, float distance, LayerMask layerMask)
    {
        // Ray를 시작 위치(현재 오브젝트 위치)에서 지정된 방향으로 발사
        Ray ray = new Ray(origin.position, direction);
        RaycastHit hit;

        // 장애물이 있을 경우
        if (Physics.Raycast(ray, out hit, distance, layerMask))
        {
            // 장애물에 부딪히면 장애물까지의 위치 반환
            return hit.point;
        }
        else
        {
            // 장애물이 없으면 지정된 거리만큼 뒤에 배치
            return origin.position + direction.normalized * distance;
        }
    }
}
