using UnityEngine;

public class Attack : State
{
    EnemyAI AI;
    public Attack(StateMachine StateMachine) : base(StateMachine)
    {
        AI = StateMachine as EnemyAI;
    }

    protected override void Start()
    {
        AI.rb.velocity = Vector2.zero;
        AI.animator.SetFloat("speed", 0);
        AI.animator.SetTrigger("attacked1");
    }

    protected override void Tick()
    {
        AI.LookLeft(AI.transform.position.x > AI.detector.health.transform.position.x);
    }
    protected override bool TransitionCondition()
    {
        if (AI.detector.health == null ||
            AI.detector.health.isDead)
            return true;


        var distance = Vector2.Distance(AI.transform.position, AI.detector.health.transform.position);
        if (distance > AI.minMeleeRange)
            return true;
        return false;
    }

    protected override State TransitionTo()
    {
        return new RunTowardsTarget(AI);
    }
}
