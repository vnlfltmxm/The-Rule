using System;
using UnityEngine;

public class MonsterManager : SingletonMonoBehaviour<MonsterManager>
{
    protected override void Init()
    {
        
    }

    public void InitStage()
    {
        //스테이지에 스폰할 몬스터 등록
    }

    private void Update()
    {
        //몬스터 스폰 대기, 스폰
    }
}
