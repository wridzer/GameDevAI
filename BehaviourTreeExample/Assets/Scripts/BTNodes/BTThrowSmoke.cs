using System.Collections;
using UnityEngine;

public class BTThrowSmoke : BTBaseNode
{
    private BlackBoard blackBoard;

    public BTThrowSmoke(BlackBoard bb)
    {
        blackBoard = bb;
    }

    public override TaskStatus Run()
    {
        blackBoard.GetValue<GameObject>("text").GetComponent<TextMesh>().text = "Trowing smoke";

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
        newBomb.GetComponent<Rigidbody>().AddForce(new Vector3(2, 5, 0), ForceMode.Impulse);
    }
}