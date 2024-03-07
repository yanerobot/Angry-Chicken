public abstract class StateStatic
{
    public bool canExit = true;

    protected StateMachineStatic stateMachine;
    public StateStatic(StateMachineStatic stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public virtual void OnEnter() { }
    public virtual void OnExit() { }
}
