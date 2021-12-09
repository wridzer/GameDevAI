using System.Collections;
using System.Linq;
using UnityEngine;

public class AISelector : MonoBehaviour
{
    private AIBehaviour[] behaviours;
    private AIBehaviour currentBehaviour;


    public void OnInitialize(BlackBoard bb)
    {
        behaviours = gameObject.GetComponents<AIBehaviour>();
        foreach (var bhv in behaviours)
        {
            bhv.OnInitialize(bb);
        }
    }

    public void EvaluateBehaviours()
    {
        var newBehaviour = behaviours.ToList().OrderByDescending(x => x.GetNormalizedScore()).First();
        if (newBehaviour != currentBehaviour)
        {
            currentBehaviour?.OnExit();
            currentBehaviour = newBehaviour;
            currentBehaviour?.OnEnter();

        }
    }
}