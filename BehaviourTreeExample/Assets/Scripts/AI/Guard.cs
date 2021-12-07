using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{
    private BTBaseNode tree;
    private NavMeshAgent agent;
    private Animator animator;

    public AISelector AISelector { get; private set; }
    public BlackBoard blackBoard { get; private set; }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();

        AISelector = GetComponent<AISelector>();
        blackBoard = GetComponent<BlackBoard>();
        //BlackBoard.onInitialized;
        AISelector.OnInitialize(blackBoard);
    }

    private void Start()
    {
        //Create your Behaviour Tree here!
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
