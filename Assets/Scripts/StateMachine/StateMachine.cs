using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    public float updateTime;

    protected State State;

    internal void SetState(State state)
    {
        State = state;
        StartCoroutine(State.PerformState());
    }

    protected void ExitState()
    {
        StopAllCoroutines();
        State = null;
    }
}
