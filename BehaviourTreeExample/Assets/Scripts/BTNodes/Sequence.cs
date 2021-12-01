using System.Collections;
using UnityEngine;

public class Sequence : BTBaseNode
{
    int index = 0;
    private BTBaseNode[] nodes;
    public Sequence(params BTBaseNode[] nodes)
    {
        this.nodes = nodes;
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
        for (; index < nodes.Length; index++)
        {
            switch (nodes[index].Run())
            {
                case TaskStatus.Success: continue;
                case TaskStatus.Failed: index = 0; return TaskStatus.Failed;
                case TaskStatus.Running: return TaskStatus.Running;
            }
        }
        index = 0;
        return TaskStatus.Success;
    }
}