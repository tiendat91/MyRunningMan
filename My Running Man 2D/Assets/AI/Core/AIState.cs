using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/State")]
public class AIState : ScriptableObject
{
    public AIAction[] Action;
    public AITransition[] Transitions;

    public void RunState(StateController controller)
    {
        ExecuteActions(controller);
        EvaluateTransition(controller);
    }
    public void ExecuteActions(StateController controller)
    {
        foreach (AIAction action in Action)
        {
            action.Act(controller);
        }
    }
    public void EvaluateTransition(StateController controller)
    {
        if (Transitions != null || Transitions.Length > 0)
        {
            for (int i = 0; i < Transitions.Length; i++)
            {
                bool decisionValue = Transitions[i].Decision.Decide(controller);
                if (decisionValue)
                {
                    controller.TransitionToState(Transitions[i].TrueState);
                }
                else
                {
                    controller.TransitionToState(Transitions[i].FalseState);
                }
            }
        }
    }
}
