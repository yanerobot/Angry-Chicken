using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerMain : MonoBehaviour
{
    [SerializeField] GameManager gameManager;

    [Header("Main")]
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] Transform groundChecker;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Vector2 dmgForce;
    [SerializeField] float dmgFreezeTime;
    [SerializeField] HealthBarUI healthBarUI;
    [SerializeField] CoinsCollector coins;



    [HideInInspector]
    public Health health;
    InputMaster input;
    Animator animator;
    Rigidbody2D rb;

    [Header("Movement")]
    [SerializeField] float normalModeSpeed;
    [SerializeField] float forceModeSpeed;
    [SerializeField] float jumpHeight;
    [SerializeField] Vector2 wallJumpForce;
    [SerializeField] Transform lookDir;
    [SerializeField] float accelerationClampValue;

    internal bool isGrounded;
    internal bool isAttached;
    float currentSpeed;
    float lastDir;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSrc;
    [SerializeField] private AudioClipOneShot flapSound;
    [SerializeField] private float flapDelay = 0.1f;
    float curFlapDelay;


    public float InputX => input.Keyboard.Run.ReadValue<float>();
    public float CurrentVelocityX => rb.velocity.x;

    void Awake()
    {
        input = new InputMaster(); 
        input.Keyboard.Jump.performed += ctx => Jump();
        input.Keyboard.Jump.performed += ctx => WallJump();
        input.Keyboard.Eat.performed += ctx => Eat(true);
        input.Keyboard.Eat.canceled += ctx => Eat(false);

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
    }

    void OnEnable()
    {
        Init();
    }

    void OnDisable()
    {
        health.OnDamage -= TakeDamage;
        health.OnDie -= Die;
        input.Disable();
    }

    void FixedUpdate()
    {
        isGrounded = GroundCheck();

        animator.SetBool("isInAir", !isGrounded);

        curFlapDelay += Time.deltaTime;
        if (isGrounded == false && curFlapDelay > flapDelay)
        {
            curFlapDelay = 0;
            audioSrc.PlayOneShot(flapSound.clip, flapSound.volume + UnityEngine.Random.Range(-flapSound.randomizer, flapSound.randomizer));
        }
    }

    public void Init()
    {
        health.OnDamage += TakeDamage;
        health.OnDie += Die;

        health.Init();
        healthBarUI.Init();
        coins.Init();

        FreePosition();
        StartCoroutine(ForceRun());

        input.Enable();
    } 

    public void Terminate()
    {
        health.OnDamage -= TakeDamage;
        health.OnDie -= Die;

        StopAllCoroutines(); 
        CancelInvoke();

        input.Disable();
    }

    bool GroundCheck()
    {
        if (rb.velocity.y > 1) 
            return false;

        RaycastHit2D hit = Physics2D.Raycast(groundChecker.position, Vector2.down, 0.1f, groundLayer);

        if (hit.collider != null)
        {
            return true;
        }
        return false;
    }
    IEnumerator ForceRun()
    {
        var inputX = 0f;
        while (true)
        {
            inputX = input.Keyboard.Run.ReadValue<float>();
            FlipSprite(inputX);

            if (isAttached ||
                rb.velocity.x > normalModeSpeed && inputX > 0 ||
                rb.velocity.x < -normalModeSpeed && inputX < 0)
            {
                yield return null;
                continue;
            }

            animator.SetBool("isRunning", Mathf.Abs(rb.velocity.x) > .5f);
            var dir = inputX;
            if (inputX == 0 && rb.velocity.x != 0)
            {
                if (Mathf.Abs(rb.velocity.x) > .5f)
                {
                    dir = Mathf.Sign(-rb.velocity.x);
                }
            }
            rb.AddForce(Vector2.right * dir * forceModeSpeed, ForceMode2D.Impulse);
            if (dir == 0) rb.velocity = rb.velocity.WhereX(0);
            yield return null;
        }
    }

    void Jump()
    {
        if (!isGrounded || isAttached) return;
        
        rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
    }
    void WallJump()
    {
        if (!isAttached)
            return;

        UnAttach();
        var dir = -Mathf.Sign(lastDir);
        rb.AddForce(wallJumpForce.Multiply(x: dir), ForceMode2D.Impulse);
        FlipSprite(dir);
    }
    void Eat(bool flag)
    {
        if (!isGrounded) return;

        animator.SetBool("Eating", flag);
    }
    void FlipSprite(float inputX)
    {
        if (inputX == 0 || Mathf.Sign(lastDir) == Mathf.Sign(inputX) || isAttached)
            return;

        lastDir = inputX;
        sprite.flipX = !sprite.flipX;
        lookDir.eulerAngles = lookDir.eulerAngles.AddTo(y: -180);
    }
    void TakeDamage(int amount)
    {
        animator.SetTrigger("Damaged");
        Freeze();
        rb.velocity = Vector2.zero;
        rb.AddForce(dmgForce.WhereX(-Mathf.Sign(lastDir) * dmgForce.x), ForceMode2D.Impulse);
        CancelInvoke(nameof(UnFreeze));
        Invoke(nameof(UnFreeze), dmgFreezeTime);
    }
    void Die()
    {
        Terminate();
        FreePosition();
        animator.SetTrigger("Dead");
    }

    void OnGameOver()
    {
        gameManager.canvasHandler.ShowGameOverScreen(true);
    }

    public void Freeze()
    {
        input.Disable();
    }

    public void UnFreeze()
    {
        input.Enable();
    }

    public void AttachToWall(RaycastHit2D hit)
    {
        if (isGrounded) return;

        transform.Translate(lastDir * (hit.distance - 0.1f), 0, 0);

        isAttached = true;
        animator.SetBool("Attached", true);
        FixPosition();
    }
    void UnAttach()
    {
        isAttached = false;
        animator.SetBool("Attached", false);
        FreePosition();
        Freeze();
        Invoke(nameof(UnFreeze), 0.2f);
    }
    public void FixPosition()
    {
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;
    }    
    public void FreePosition()
    {
        rb.isKinematic = false;
    }
}
