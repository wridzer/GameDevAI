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
    private bool isAttacked = false;
    [SerializeField] private GameObject text;

    public GameObject guardInstance;
    public GameObject playerInstance;
    public GameObject[] walls;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        blackBoard = new BlackBoard();
        EventManager<bool>.Subscribe(EventType.PLAYER_ATTACKED, PlayerAttacked);
    }

    private void Start()
    {
        blackBoard.SetValue<GameObject>("text", text);
        blackBoard.SetValue<float>("wallOffset", 2);
        blackBoard.SetValue<GameObject[]>("walls", walls);
        blackBoard.SetValue<NavMeshAgent>("navMeshAgent", agent);
        BTBaseNode protecc =
            new Sequence(
                new BTCheckOnPlayer(blackBoard),
                new BTFindCover(blackBoard),
                new BTWalkToDest(blackBoard),
                new BTThrowSmoke(blackBoard)
                );
        BTBaseNode follow =
            new Sequence(
                new BTCheckPlayerDist(blackBoard),
                new BTWalkToDest(blackBoard)
                );
        tree =
            new BTSelector(
                protecc,
                follow
                );
    }

    private void FixedUpdate()
    {
        blackBoard.SetValue<bool>("playerAttacked", isAttacked);
        blackBoard.SetValue<Vector3>("myPos", gameObject.transform.position);
        blackBoard.SetValue<Vector3>("guardPos", guardInstance.transform.position);
        blackBoard.SetValue<Vector3>("playerPos", playerInstance.transform.position);
        tree?.Run();
    }

    public void PlayerAttacked(bool _arg1)
    {
        isAttacked = _arg1;
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
