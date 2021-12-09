using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "FloatEvaluator", menuName = "Evaluators/FloatEvaluator")]
public class FloatEvaluator : UtilityEvaluator
{
    private FloatValue floatValue;
    public string floatName;

    public override float GetMaxValue()
    {
        return floatValue.MaxValue;
    }

    public override float GetValue()
    {
        return floatValue.Value;
    }

    public override void OnInitialize(BlackBoard bb)
    {
        floatValue = bb.GetValue<FloatValue>(floatName);
    }
}