using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyPlatform : MonoBehaviour
{
    [SerializeField] float force;
    [SerializeField] Vector2 worldForceDirection;
    [Header("Audio Effect")]
    [SerializeField] AudioSource src;
    [SerializeField, Range(0, 0.5f)] float pitchRandomizer;

    Animator animator;

    Vector2 forceDirection;

    void Awake()
    {
        
        animator = GetComponent<Animator>();
        forceDirection = transform.up;
        if (worldForceDirection != Vector2.zero)
            forceDirection = worldForceDirection.normalized;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Rigidbody2D rb))
        {
            src.pitch += Random.Range(-pitchRandomizer, pitchRandomizer);
            src.PlayOneShot(src.clip);
            rb.velocity = rb.velocity.WhereY(0);
            rb.AddForce(forceDirection * force, ForceMode2D.Impulse);
            animator.SetTrigger("Open");
        }
    }
}
