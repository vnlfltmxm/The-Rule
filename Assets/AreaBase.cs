using System;
using System.Collections.Generic;
using Script.Util;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class AreaBase : MonoBehaviour
{
    [SerializeField] public AreaType AreaType;
    [SerializeField] public NavMeshModifierVolume NavMeshModifierVolume;
    [SerializeField] public List<Transform> SpawnPosList = new List<Transform>();

    private void Start()
    {
        StageManager.Instance.AddArea(this);
    }
    
    /*private Vector3 GetRandomPosition()
    {
        Vector3 center = transform.position;
        Vector3 scale = transform.localScale;
        
        // NavMesh에서 해당 위치가 유효한지 확인
        if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, range, NavMesh.AllAreas))
        {
            return hit.position; // 유효한 위치 반환
        }
        return transform.position; // 유효한 위치를 찾지 못하면 transform.position 반환
    }*/
    
    private void OnValidate()
    {
        if (NavMeshModifierVolume == null)
        {
            NavMeshModifierVolume = GetComponent<NavMeshModifierVolume>();
            if (NavMeshModifierVolume == null)
                NavMeshModifierVolume = gameObject.AddComponent<NavMeshModifierVolume>();
        }
    }

    public void LoadSpawnPos()
    {
        SpawnPosList.Clear();
        Transform spawnPos = transform.Find("SpawnPos");
        foreach (Transform child in spawnPos)
        {
            SpawnPosList.Add(child);
        }
    }
}
