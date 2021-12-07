using System;
using System.Linq;
using UnityEngine;

public abstract class AIBehaviour : MonoBehaviour
{
    [SerializeField] public UtilityEvaluator[] utilities;

    public void OnInitialize(BlackBoard bb)
    {
        foreach (var utility in utilities)
        {
            utility.OnInitialize(bb);
        }    
    }

    public float GetNormalizedScore()
    {
        return Mathf.Clamp01(utilities.ToList().Sum(x => x.GetNormalizedScore()) / utilities.Length);
    }

    internal void OnExit()
    {
        throw new NotImplementedException();
    }

    internal void OnEnter()
    {
        throw new NotImplementedException();
    }
}