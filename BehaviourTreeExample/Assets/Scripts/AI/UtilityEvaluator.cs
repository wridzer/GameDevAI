using System;
using System.Collections;
using UnityEngine;

public abstract class UtilityEvaluator : BaseScriptableObject
{
    public Type VariableType;

    [SerializeField] public AnimationCurve evaluationCurve;
    public abstract void OnInitialize(BlackBoard bb);
    public abstract float GetValue();
    public abstract float GetMaxValue();
    public float  GetNormalizedScore()
    {
        return Mathf.Clamp01(evaluationCurve.Evaluate(GetValue() / GetMaxValue()));
    }
}