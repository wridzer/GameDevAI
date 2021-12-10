using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;


public class USPatrol : AIBehaviour
{
    [SerializeField] private float stoppingDistance = 2;
    private Vector3[] petrolPoints;
    private Vector3 destination;
    private Vector3 myPos;
    private NavMeshAgent navMeshAgent;
    private int pointCounter;

    public override void Execute()
    {
        if(destination == Vector3.zero)
        {
            destination = petrolPoints[pointCounter];
        }
        if (Vector3.Distance(myPos, destination) < stoppingDistance)
        {
            pointCounter++;
            if(pointCounter >= petrolPoints.Length)
            {
                pointCounter = 0;
            }
            destination = petrolPoints[pointCounter];
        }
        navMeshAgent.destination = destination;
        myPos = blackBoard.GetValue<Vector3>("myPos");
    }

    public override void OnEnter()
    {
        blackBoard.GetValue<GameObject>("text").GetComponent<TextMesh>().text = "Patrolling";
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