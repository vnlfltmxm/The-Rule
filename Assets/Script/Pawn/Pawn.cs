using System;
using Script.Util;
using UnityEngine;
using UnityEngine.AI;

namespace Script.Pawn
{
    public abstract class Pawn : MonoBehaviour
    {
        public AreaType CurrentArea = AreaType.Null;
        
        private void Start()
        {
            Init();
        }

        protected abstract void Init();
        
        
        private void FixedUpdate()
        {
            CurrentArea = GetCurrentArea();
            OnFixedUpdate();
        }

        protected virtual void OnFixedUpdate() { }

        //강제 이벤트시 조작 막는 용도
        public abstract void SetActiveForcedEvent(bool isActive);
        
        public AreaType GetCurrentArea()
        {
            NavMeshHit hit;
            if (NavMesh.SamplePosition(transform.position, out hit, 0.1f, NavMesh.AllAreas))
            {
                int areaIndex = hit.mask; // 현재 위치한 NavMesh Area Index

                AreaType areaType = (AreaType)areaIndex;
                //AreaType areaType = GetAreaTypeFromID(areaIndex);
                return areaType;
            }
            return AreaType.Null; // 해당하는 Area 없음
        }
    }
}