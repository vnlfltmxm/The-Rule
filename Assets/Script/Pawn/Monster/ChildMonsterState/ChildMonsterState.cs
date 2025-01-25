using UnityEngine;

public class ChildMonsterState
{
    public ChildMonsterState(ChildMonster childMonster)
    {
        this.childMonster = childMonster;
        state = childMonster.GetComponent<ChildStateMachine>();
    }

    protected ChildMonster childMonster;
    protected ChildStateMachine state;
}
