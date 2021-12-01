using System.Collections;
using UnityEngine;

public class BTThrowSmoke : BTBaseNode
{
    private BlackBoard blackBoard;

    public BTThrowSmoke(BlackBoard bb)
    {
        blackBoard = bb;
    }

    public override TaskStatus Run()
    {
        Debug.Log("Throwing smoke to: " + blackBoard.GetValue<Vector3>("guardPos"));
        return TaskStatus.Success;
    }
    public override void OnEnter()
    {
        throw new System.NotImplementedException();
    }

    public override void OnExit()
    {
        throw new System.NotImplementedException();
    }
}