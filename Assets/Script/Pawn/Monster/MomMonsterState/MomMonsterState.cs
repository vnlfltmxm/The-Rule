using UnityEngine;

public class MomMonsterState
{
    public MomMonsterState(MomMonster momMonster)
    {
        this.momMonster = momMonster;
        state = momMonster.GetComponent<MomStateMachine>();
    }

    protected MomMonster momMonster;
    protected MomStateMachine state;
}
