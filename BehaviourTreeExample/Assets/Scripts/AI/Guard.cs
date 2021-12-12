using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Guard : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    [SerializeField] private GameObject playerInstance;
    [SerializeField] private float maxDistance;
    [SerializeField] private float maxHealth;
    [SerializeField] private float memoryTimer;
    [SerializeField] private float ViewAngleInDegrees = 30;
    [SerializeField] private float SightRange = 10;
    [SerializeField] private float changeHealthSpeed = 2;
    [SerializeField] private GameObject text;
    [SerializeField] private Slider healthbar;
    private bool playerSeen = false;
    private float timer = 0;

    public AISelector AISelector { get; private set; }
    private BlackBoard blackBoard;
    private FloatValue health;
    private FloatValue distance;
    private bool spottedPlayer;

    private Vector3[] petrolPositions = { new Vector3(-6, 0, -5), new Vector3(6, 0, -5), new Vector3(6, 0, 8), new Vector3(-6, 0, 8) };


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();

        AISelector = GetComponent<AISelector>();
        blackBoard = new BlackBoard();
        blackBoard.SetValue<NavMeshAgent>("navMeshAgent", agent);
        blackBoard.SetValue<Vector3[]>("petrolPoints", petrolPositions);
        blackBoard.SetValue<GameObject>("playerInstance", playerInstance);
        blackBoard.SetValue<GameObject>("text", text);
    }

    private void Start()
    {
        //set health
        health = new FloatValue();
        health.MaxValue = maxHealth;
        health.Value = health.MaxValue;
        blackBoard.SetValue<FloatValue>("health", health);

        //set distance
        distance = new FloatValue();
        distance.MaxValue = maxDistance;
        distance.Value = Vector3.Distance(transform.position, playerInstance.transform.position);
        blackBoard.SetValue<FloatValue>("distance", distance);

        //set viewed
        blackBoard.SetValue<bool>("playerSeen", false);

        AISelector.OnInitialize(blackBoard);
    }

    private void FixedUpdate()
    {
        health.Value += Input.GetAxis("Mouse ScrollWheel") * changeHealthSpeed;
        healthbar.value = health.Value;
        blackBoard.SetValue<FloatValue>("health", health);
        bool playerSeen = CheckForPlayer();
        blackBoard.SetValue<bool>("playerSeen", playerSeen);
        if (playerSeen)
        {
            distance.Value = Vector3.Distance(transform.position, playerInstance.transform.position);
        }
        else
        {
            distance.Value = distance.MaxValue;
        }
        blackBoard.SetValue<FloatValue>("distance", distance);
        blackBoard.SetValue<Vector3>("myPos", transform.position);
        AISelector.EvaluateBehaviours();
        AISelector.ExecuteBehaviour();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Handles.color = Color.yellow;
        Vector3 endPointLeft = transform.position + (Quaternion.Euler(0, -ViewAngleInDegrees, 0) * transform.forward).normalized * SightRange;
        Vector3 endPointRight = transform.position + (Quaternion.Euler(0, ViewAngleInDegrees, 0) * transform.forward).normalized * SightRange;

        Handles.DrawWireArc(transform.position, Vector3.up, Quaternion.Euler(0, -ViewAngleInDegrees, 0) * transform.transform.forward, ViewAngleInDegrees * 2, SightRange);
        Gizmos.DrawLine(transform.position, endPointLeft);
        Gizmos.DrawLine(transform.position, endPointRight);
    }

    private bool CheckForPlayer()
    {
        if(Vector3.Distance(transform.position, playerInstance.transform.position) < maxDistance)
        {
            //get player angle
            float playerAngle = Quaternion.Angle(Quaternion.LookRotation(transform.forward), Quaternion.LookRotation(playerInstance.transform.position - transform.position));
            if(playerAngle < ViewAngleInDegrees && playerAngle > -ViewAngleInDegrees)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, playerInstance.transform.position - transform.position, out hit, SightRange))
                {
                    if (hit.collider.tag == "Player")
                    {
                        Debug.DrawRay(transform.position, hit.point, Color.yellow);
                        blackBoard.SetValue<Vector3>("lastSeenPlayerPos", playerInstance.transform.position);
                        playerSeen = true;
                        timer = memoryTimer;
                    }
                }
            }
        }
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }else {
            playerSeen = false;
        }
        if (playerSeen) { 
            return true;
        }
        else { return false; }
    }
}
