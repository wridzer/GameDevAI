using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class USFlee : AIBehaviour
{
    [SerializeField] private float fleeDist = 20;
    [SerializeField] private float healAmount = 5;

    private Vector3 destination;
    private Vector3 myPos;
    private NavMeshAgent navMeshAgent;
    private GameObject playerInstance;
    private FloatValue health;

    public override void Execute()
    {
        myPos = blackBoard.GetValue<Vector3>("myPos");
        if(Vector3.Distance(myPos, playerInstance.transform.position) < fleeDist)
        {
            Vector3 direction = (myPos - playerInstance.transform.position).normalized;
            destination = direction + myPos;
            navMeshAgent.destination = destination;
        }
        else
        {
            Heal();
        }
    }

    public override void OnEnter()
    {
        blackBoard.GetValue<GameObject>("text").GetComponent<TextMesh>().text = "Fleeing";
        navMeshAgent = blackBoard.GetValue<NavMeshAgent>("navMeshAgent");
        playerInstance = blackBoard.GetValue<GameObject>("playerInstance");
        myPos = blackBoard.GetValue<Vector3>("myPos");
        health = blackBoard.GetValue<FloatValue>("health");
    }

    public override void OnExit()
    {
        destination = Vector3.zero;
    }

    private void Heal()
    {
        health.Value += healAmount * Time.deltaTime;
    }
}
