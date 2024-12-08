using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InvadeObject : MonoBehaviour
{
    [Header("MyPatrolArea")]
    [SerializeField] private string _areaName;

    public string AreaName => _areaName;

    public NavMeshPath Path { get; set; }
    public List<Vector3> PathList { get; set; }

    private void OnDrawGizmos()
    {
        if(PathList == null) return;

        Gizmos.color = Color.red;

        for (int i = 0; i < PathList.Count; i++)
        {
            Gizmos.DrawWireSphere(PathList[i], 0.2f);

            if(i < PathList.Count - 1)
            {
                Gizmos.color = Color.white;

                Gizmos.DrawLine(PathList[i], PathList[i + 1]);
            }
        }
       

       

    }
}
