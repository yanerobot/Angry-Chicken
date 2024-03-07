using UnityEngine;
internal class TakeDamage : State
{
    EnemyAI AI;
    public TakeDamage(StateMachine StateMachine) : base(StateMachine)
    {
        AI = StateMachine as EnemyAI;
    }

    protected override void Start()
    {
        AI.animator.SetTrigger("damagedMelee");
    }

    protected override bool TransitionCondition()
    {
        return true;
    }

    protected override State TransitionTo()
    {
        return new Idle(AI);
    }
}