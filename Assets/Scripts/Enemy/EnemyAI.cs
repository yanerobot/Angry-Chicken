using UnityEngine;
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyAI : StateMachine
{
    [SerializeField] private Transform meleePoint;
    [SerializeField] private float attackRadius = 0.3f;
    [SerializeField] private float attackDistance = 0.1f;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] float runSpeed;


    public float minMeleeRange;
    public float minXVelocity;
    public PlayerDetector detector;

    [HideInInspector] public Animator animator;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Vector2 initialPos;
    [HideInInspector] Health health;


    void Start()
    {
        initialPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();
        health.Init();
        health.OnDamage += TakeDamage;
        health.OnDie += StartDying;

        SetState(new Idle(this));
    }

    internal void LookLeft(bool flag)
    {
        if (flag)
            transform.localEulerAngles = transform.localEulerAngles.WhereY(180);
        else
            transform.localEulerAngles = transform.localEulerAngles.WhereY(0);
    }

    void TakeDamage(int amount)
    {
        ExitState();
        SetState(new TakeDamage(this));
    }

    void StartDying()
    {
        ExitState();
        animator.SetBool("isDead", true);
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void OnAtack()
    {
        audioSource.Play();

        var hits = Physics2D.CircleCastAll(meleePoint.position, attackRadius, Vector2.right, attackDistance);

        if (hits.Length == 0) return;

        foreach (var hit in hits)
        {
            if (hit.transform.gameObject.layer == 10 || hit.transform.gameObject.layer == 9)
                continue;
            if (hit.transform.TryGetComponent(out Switcher activator))
                activator.Activate();
            if (hit.transform.TryGetComponent(out Health health))
            {
                health.TakeDamage(1);
            }
        }
    }

    public void RunTowards(Vector2 pos)
    {
        var direction = transform.Direction(pos);

        rb.velocity = new Vector2(Mathf.Sign(direction.x) * runSpeed, rb.velocity.y);
    }
}
