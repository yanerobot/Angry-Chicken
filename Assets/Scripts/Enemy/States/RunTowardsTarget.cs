using UnityEngine;
public class RunTowardsTarget : State
{
    EnemyAI AI;
    Vector2 target;
    public RunTowardsTarget(StateMachine StateMachine) : base(StateMachine)
    {
        AI = StateMachine as EnemyAI;
    }

    protected override void Start()
    {
        if (AI.detector.health != null && AI.detector.health.transform.position.y != AI.transform.position.y)
        {
            AI.animator.SetBool("TargetPlayer", true);
            target = AI.detector.health.transform.position;
        }
        else
        {
            AI.animator.SetBool("TargetPlayer", false);
            target = AI.initialPos;
        }
        
    }
    protected override void Tick()
    {
        AI.animator.SetFloat("speed", Mathf.Abs(AI.rb.velocity.x));

        if (AI.detector.health != null && AI.detector.health.transform.position.y != AI.transform.position.y)
        {
            AI.animator.SetBool("TargetPlayer", true);
            target = AI.detector.health.transform.position;
        }
        else
        {
            AI.animator.SetBool("TargetPlayer", false);
            target = AI.initialPos;
        }
        
        AI.LookLeft(AI.transform.position.x > target.x);

        AI.RunTowards(target);
    }
    protected override bool TransitionCondition()
    {
        var distance = Vector2.Distance(AI.transform.position, target);

        if (distance <= AI.minMeleeRange) return true;
        else if (Mathf.Abs(AI.rb.velocity.x) < AI.minXVelocity) return true;

        return false;
    }

    protected override State TransitionTo()
    {
        if (target == AI.initialPos)
            return new Idle(AI);

        return new Attack(AI);
    }
}
