﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    [SerializeField] private GameObject playerInstance;

    public AISelector AISelector { get; private set; }
    private BlackBoard blackBoard;
    private FloatValue health;
    private FloatValue distance;

    private Vector3[] petrolPositions = { new Vector3(0, 0, 0), new Vector3(6, 0, -5), new Vector3(6, 0, 8), new Vector3(-6, 0, 0) };


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();

        AISelector = GetComponent<AISelector>();
        blackBoard = new BlackBoard();
        blackBoard.SetValue<NavMeshAgent>("navMeshAgent", agent);
        blackBoard.SetValue<Vector3[]>("petrolPoints", petrolPositions);
    }

    private void Start()
    {
        health = new FloatValue();
        health.MaxValue = 100;
        health.Value = health.MaxValue;
        blackBoard.SetValue<FloatValue>("health", health);

        distance = new FloatValue();
        distance.MaxValue = 20;
        distance.Value = Vector3.Distance(transform.position, playerInstance.transform.position);
        blackBoard.SetValue<FloatValue>("distance", distance);
        AISelector.OnInitialize(blackBoard);
    }

    private void FixedUpdate()
    {
        //tree?.Run();
        distance.Value = Vector3.Distance(transform.position, playerInstance.transform.position);
        blackBoard.SetValue<FloatValue>("distance", distance);
        blackBoard.SetValue<Vector3>("myPos", transform.position);
        AISelector.EvaluateBehaviours();
        AISelector.ExecuteBehaviour();
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
