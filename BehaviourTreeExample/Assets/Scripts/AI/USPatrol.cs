using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;


public class USPatrol : AIBehaviour
{
    private BlackBoard blackBoard;
    private Vector3[] petrolPoints;
    private Vector3 destination;
    private Vector3 myPos;
    private NavMeshAgent navMeshAgent;
    private int pointCounter;

    public override void Execute()
    {
        if (myPos == destination)
        {
            pointCounter = pointCounter + 1 % petrolPoints.Length;
            destination = petrolPoints[pointCounter];
        }
    }

    public override void OnEnter()
    {
        navMeshAgent = blackBoard.GetValue<NavMeshAgent>("navMeshAgent");
        petrolPoints = blackBoard.GetValue<Vector3[]>("petrolPoints");
        myPos = blackBoard.GetValue<Vector3>("myPos");
        destination = petrolPoints.ToList().OrderByDescending(x => Vector3.Distance(myPos, x)).First();
    }
}