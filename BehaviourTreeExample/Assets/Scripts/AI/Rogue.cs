using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;

public class Rogue : MonoBehaviour
{

    private BTBaseNode tree;
    private NavMeshAgent agent;
    private Animator animator;
    private BlackBoard blackBoard;

    public GameObject guardInstance;
    public GameObject[] walls;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        blackBoard = new BlackBoard();
    }

    private void Start()
    {
        //TODO: Create your Behaviour tree here
        blackBoard.SetValue<bool>("playerAttacked", true); //set to true for debug purposes, change later
        blackBoard.SetValue<Vector3>("myPos", gameObject.transform.position);
        blackBoard.SetValue<Vector3>("guardPos", guardInstance.transform.position);
        blackBoard.SetValue<float>("wallOffset", 2);
        blackBoard.SetValue<GameObject[]>("walls", walls);
        blackBoard.SetValue<NavMeshAgent>("navMeshAgent", agent);
        tree =
            new Sequence(
                new BTCheckOnPlayer(blackBoard),
                new BTFindCover(blackBoard),
                new BTGoToCover(blackBoard),
                new BTThrowSmoke(blackBoard)
                );
        Debug.Log(blackBoard.GetValue<Vector3>("coverPosition"));
    }

    private void FixedUpdate()
    {
        tree?.Run();
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.yellow;
    //    Handles.color = Color.yellow;
    //    Vector3 endPointLeft = viewTransform.position + (Quaternion.Euler(0, -ViewAngleInDegrees.Value, 0) * viewTransform.transform.forward).normalized * SightRange.Value;
    //    Vector3 endPointRight = viewTransform.position + (Quaternion.Euler(0, ViewAngleInDegrees.Value, 0) * viewTransform.transform.forward).normalized * SightRange.Value;

    //    Handles.DrawWireArc(viewTransform.position, Vector3.up, Quaternion.Euler(0, -ViewAngleInDegrees.Value, 0) * viewTransform.transform.forward, ViewAngleInDegrees.Value * 2, SightRange.Value);
    //    Gizmos.DrawLine(viewTransform.position, endPointLeft);
    //    Gizmos.DrawLine(viewTransform.position, endPointRight);

    //}
}
