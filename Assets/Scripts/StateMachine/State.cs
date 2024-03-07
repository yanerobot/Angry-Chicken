using UnityEngine;
using System.Collections;

public abstract class State
{
    protected WaitForSeconds update;

    protected StateMachine StateMachine;
    protected State(StateMachine StateMachine)
    {
        this.StateMachine = StateMachine;
        update = new WaitForSeconds(StateMachine.updateTime);
    }

    public IEnumerator PerformState()
    {
        Start();
        yield return update;

        while (!TransitionCondition())
        {
            Tick();
            yield return update;
        }

        StateMachine.SetState(TransitionTo());
    }
    protected virtual bool Exception()
    {
        return false;
    }
    protected virtual void Start() { }
    protected virtual void Tick() { }
    protected abstract bool TransitionCondition();
    protected abstract State TransitionTo();

}