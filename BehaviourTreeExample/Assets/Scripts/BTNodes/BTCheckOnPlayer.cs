using System.Collections;
using UnityEngine;

public class BTCheckOnPlayer : BTBaseNode
{
    private BlackBoard blackBoard;

    public BTCheckOnPlayer(BlackBoard bb)
    {
        blackBoard = bb;
    }

    public override TaskStatus Run()
    {
        if(blackBoard.GetValue<bool>("playerAttacked") == true)
        {
            return TaskStatus.Success;
        }

        return TaskStatus.Failed;
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