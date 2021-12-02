using System.Collections;
using UnityEngine;

public class BTCheckPlayerDist : BTBaseNode
{
    private BlackBoard blackBoard;

    public BTCheckPlayerDist(BlackBoard bb)
    {
        blackBoard = bb;
    }

    public override void OnEnter()
    {
        throw new System.NotImplementedException();
    }

    public override void OnExit()
    {
        throw new System.NotImplementedException();
    }

    public override TaskStatus Run()
    {
        Vector3 playerPos = blackBoard.GetValue<Vector3>("playerPos");
        float stoppingDist = blackBoard.GetValue<float>("stoppingDist");


        if (Vector3.Distance(blackBoard.GetValue<Vector3>("myPos"), playerPos) <= blackBoard.GetValue<float>("stoppingDist"))
        {
            return TaskStatus.Failed;
        } else
        {
            Vector3 dest = playerPos - new Vector3(stoppingDist, 0, stoppingDist);
            blackBoard.SetValue<Vector3>("destination", dest);
            return TaskStatus.Success;
        }
    }
}