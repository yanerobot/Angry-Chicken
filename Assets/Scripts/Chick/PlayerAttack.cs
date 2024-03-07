using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAttack : MonoBehaviour
{
    [SerializeField] GameObject eggPrefab;
    [SerializeField] float eggThrowForce;
    [SerializeField] Transform shootingPoint;
    [SerializeField] Transform meleePoint;
    [SerializeField] string detectorTag;
    [SerializeField] private float meleeRadius, meleeDistance;
    [SerializeField] private AudioSource aSrc;

    [SerializeField] float eggRotation;
    

    Animator animator;

    PlayerMain player;

    InputMaster input;


    void Start()
    {
        input = new InputMaster();
        input.Enable();

        animator = GetComponent<Animator>();
        player = GetComponent<PlayerMain>();
        input.Keyboard.Strike.performed += ctx => Melee();
        input.Keyboard.ThrowEgg.performed += ctx => Range();
    }

    void FixedUpdate()
    {
        Debug.DrawRay(meleePoint.position, meleePoint.right, Color.white);
    }

    void Melee()
    {
        animator.SetTrigger("Melee");
    }

    void Range()
    {
        animator.SetTrigger("Range");
    }

    public void ThrowEgg()
    {
        if (eggPrefab == null)
        {
            print("No egg prefab");
            return;
        }
        var egg = Instantiate(eggPrefab);
        egg.transform.position = shootingPoint.position;
        var eggRb = egg.GetComponent<Rigidbody2D>();
        eggRb.AddForce(shootingPoint.right * eggThrowForce, ForceMode2D.Impulse);
        eggRb.AddTorque(eggRotation);
    }

    public void OnAttack()
    {
        aSrc.Play();
        var hits = Physics2D.CircleCastAll(meleePoint.position, meleeRadius, meleePoint.transform.right, meleeDistance);
        if (hits.Length == 0) return;

        foreach (var hit in hits)
        {
            if (hit.transform == transform) 
                continue;
            if (hit.transform.CompareTag(detectorTag))
                continue;
            if (hit.transform.gameObject.layer == 3)
                player.AttachToWall(hit);
            if (hit.transform.TryGetComponent(out Switcher activator))
                activator.Activate();
            if (hit.transform.TryGetComponent(out Health health))
                health.TakeDamage(1);
        }
    }
}
