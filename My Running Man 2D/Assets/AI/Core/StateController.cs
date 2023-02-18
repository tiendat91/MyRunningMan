using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StateController : MonoBehaviour
{
    [Header("State")]
    [SerializeField]
    private AIState currentState;
    [SerializeField]
    private AIState remainState;

    public PathFollow Path { get; set; }

    public void Start()
    {
        Path = GetComponent<PathFollow>();
    }

    private void Update()
    {
        currentState.RunState(this);
    }

    public void TransitionToState(AIState Newstate)
    {
        if (Newstate != remainState)
        {
            currentState = Newstate;
        }
    }
}


