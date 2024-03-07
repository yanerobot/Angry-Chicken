using UnityEngine;
public class Idle : State
{
    EnemyAI AI;
    public Idle(StateMachine StateMachine) : base(StateMachine)
    {
        AI = StateMachine as EnemyAI;
    }

    protected override void Start()
    {
        AI.rb.velocity = Vector2.zero;
        AI.animator.SetFloat("speed", 0);
    }

    protected override bool TransitionCondition()
    {
        return AI.detector.health != null;
    }

    protected override State TransitionTo()
    {
        return new RunTowardsTarget(AI);
    }
}
