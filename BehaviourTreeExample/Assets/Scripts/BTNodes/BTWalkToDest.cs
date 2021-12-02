using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BTWalkToDest : BTBaseNode
{
    private BlackBoard blackBoard;

    public BTWalkToDest(BlackBoard bb)
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
        Vector3 myPos = blackBoard.GetValue<Vector3>("myPos");
        Vector3 targetPos = blackBoard.GetValue<Vector3>("destination");
        NavMeshAgent agent = blackBoard.GetValue<NavMeshAgent>("navMeshAgent");

        agent.destination = targetPos;

        if (agent.pathPending)
        {
            return TaskStatus.Running;
        }

        if (agent.isPathStale)
        {
            return TaskStatus.Failed;
        }

        switch (agent.pathStatus)
        {
            case NavMeshPathStatus.PathComplete: return TaskStatus.Success;
            case NavMeshPathStatus.PathPartial: return TaskStatus.Failed;
            case NavMeshPathStatus.PathInvalid: return TaskStatus.Failed;
        }

        return TaskStatus.Failed;
    }
}