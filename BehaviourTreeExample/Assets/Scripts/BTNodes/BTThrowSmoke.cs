using System.Collections;
using UnityEngine;

public class BTThrowSmoke : BTBaseNode
{
    private BlackBoard blackBoard;
    private float throwTimer;

    public BTThrowSmoke(BlackBoard bb)
    {
        blackBoard = bb;
    }

    public override TaskStatus Run()
    {
        Vector3 myPos = blackBoard.GetValue<Vector3>("myPos");
        Vector3 targetPos = blackBoard.GetValue<Vector3>("destination");
        blackBoard.GetValue<GameObject>("text").GetComponent<TextMesh>().text = "Trowing smoke";
        if(throwTimer > 0)
        {
            throwTimer -= Time.deltaTime;
        }
        else if (throwTimer <= 0 && Vector3.Distance(myPos, targetPos) < 1)
        {
            throwTimer = 3;
            ThrowBomb();
        }
        //Debug.Log("Throwing smoke to: " + blackBoard.GetValue<Vector3>("guardPos"));
        return TaskStatus.Success;
    }
    public override void OnEnter()
    {
        throw new System.NotImplementedException();
    }

    public override void OnExit()
    {
        throw new System.NotImplementedException();
    }

    private void ThrowBomb()
    {
        var smokeBomb = blackBoard.GetValue<GameObject>("smokeBomb");
        var newBomb = GameObject.Instantiate(smokeBomb, blackBoard.GetValue<Vector3>("myPos"), Quaternion.Euler(new Vector3()));
        Vector3 direction = (blackBoard.GetValue<Vector3>("myPos") - blackBoard.GetValue<Vector3>("guardPos")).normalized;
        newBomb.GetComponent<Rigidbody>().AddForce(-direction * 10 + new Vector3(0, 7, 0), ForceMode.Impulse);
    }
}