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
        blackBoard.GetValue<GameObject>("text").GetComponent<TextMesh>().text = "Checking on my friend";
        if (blackBoard.GetValue<bool>("playerAttacked") == true)
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