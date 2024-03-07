using UnityEngine;
using System.Collections;

public abstract class StateMachineStatic : MonoBehaviour
{
    protected StateStatic State;
    internal void SetState(StateStatic state)
    {
        StartCoroutine(TransitionToState(state));
    }

    protected void ExitState()
    {
        StartCoroutine(TransitionToExit());
    }

    IEnumerator TransitionToExit()
    {
        if (!State.canExit)
        {
            State.OnExit();
            yield return new WaitUntil(() => State.canExit);
        }
        State = null;
    }
    IEnumerator TransitionToState(StateStatic state)
    {
        if (State != null)
        {
            State.OnExit();
            yield return new WaitUntil(() => State.canExit);
        }
        print(state);
        State = state;
        State.OnEnter();
    }
}
