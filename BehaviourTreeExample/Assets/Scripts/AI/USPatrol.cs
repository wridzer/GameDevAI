using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;


public class USPatrol : AIBehaviour
{
    private Vector3[] petrolPoints;
    private Vector3 destination;
    private Vector3 myPos;
    private NavMeshAgent navMeshAgent;
    private int pointCounter;

    public override void Execute()
    {
        if(destination == Vector3.zero)
        {
            destination = petrolPoints.ToList().OrderByDescending(x => Vector3.Distance(myPos, x)).First();
        }
        if (myPos.x == destination.x && myPos.z == destination.z)
        {
            pointCounter = (pointCounter + 1) % petrolPoints.Length;
            destination = petrolPoints[pointCounter];
        }
        navMeshAgent.destination = destination;
        myPos = blackBoard.GetValue<Vector3>("myPos");
    }

    public override void OnEnter()
    {
        Debug.Log("Patrolling");
        navMeshAgent = blackBoard.GetValue<NavMeshAgent>("navMeshAgent");
        petrolPoints = blackBoard.GetValue<Vector3[]>("petrolPoints");
        myPos = blackBoard.GetValue<Vector3>("myPos");
        pointCounter = 0;
    }

    public override void OnExit()
    {
        destination = Vector3.zero;
    }
}