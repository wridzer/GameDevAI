using System.Collections;
using UnityEngine;

public class BTFindCover : BTBaseNode
{
    private BlackBoard blackBoard;

    public BTFindCover(BlackBoard bb)
    {
        blackBoard = bb;
    }

    public override void OnEnter()
    {
        throw new System.NotImplementedException();
    }

    public override void OnExit()
    {
        throw new System.NotImplementedException();
    }

    public override TaskStatus Run()
    {
        blackBoard.GetValue<GameObject>("text").GetComponent<TextMesh>().text = "Looking for cover";
        GameObject closestWall = null;
        float distToWall = float.PositiveInfinity;
        Vector3 myPos = blackBoard.GetValue<Vector3>("myPos");
        Vector3 guardPos = blackBoard.GetValue<Vector3>("guardPos");
        float wallOffset = blackBoard.GetValue<float>("wallOffset");

        //look for object to block enim view
        foreach (GameObject wall in blackBoard.GetValue<GameObject[]>("walls"))
        {
            if (Vector3.Distance(myPos, wall.transform.position) < distToWall)
            {
                closestWall = wall;
            }
        }

        if(closestWall == null)
        {
            return TaskStatus.Failed;
        }

        Vector3 dir = (closestWall.transform.position - guardPos).normalized;
        Vector3 targetPos = closestWall.transform.position + (wallOffset * dir);

        Debug.Log(targetPos);

        blackBoard.SetValue<Vector3>("destination", targetPos);
        return TaskStatus.Success;
    }
}