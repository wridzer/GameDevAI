using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class USAttack : AIBehaviour
{
    [SerializeField] private float stoppingDistance = 2;
    private Vector3 destination;
    private Vector3 myPos;
    private NavMeshAgent navMeshAgent;
    private GameObject playerInstance;


    public override void OnEnter()
    {
        blackBoard.GetValue<GameObject>("text").GetComponent<TextMesh>().text = "Attacking";
        navMeshAgent = blackBoard.GetValue<NavMeshAgent>("navMeshAgent");
        myPos = blackBoard.GetValue<Vector3>("myPos");
        playerInstance = blackBoard.GetValue<GameObject>("playerInstance");
        destination = playerInstance.transform.position;
    }

    public override void OnExit()
    {
        destination = Vector3.zero;
        EventManager<bool>.Invoke(EventType.PLAYER_ATTACKED, false);
    }

    public override void Execute()
    {

        if (Vector3.Distance(myPos, destination) < stoppingDistance)
        {
            Attack();
        }
        destination = blackBoard.GetValue<Vector3>("lastSeenPlayerPos");
        navMeshAgent.destination = destination;
        transform.rotation = Quaternion.LookRotation(playerInstance.transform.position - transform.position);
        myPos = blackBoard.GetValue<Vector3>("myPos");
    }

    private void Attack()
    {
        Debug.Log("Attacking");
        EventManager<bool>.Invoke(EventType.PLAYER_ATTACKED, true);
    }
}