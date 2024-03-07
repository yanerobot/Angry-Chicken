using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(AudioSource))]
public class EggBehaivior : MonoBehaviour
{
    [SerializeField] private ParticleSystem crackEffect;
    [SerializeField, Range(0,0.5f)] private float pitchRandomizer;

    AudioSource audioSrc;
    Rigidbody2D rb;
    SpriteRenderer sr;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        audioSrc = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger) return;
        transform.SetParent(collision.transform);
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
        rb.isKinematic = true;
        audioSrc.pitch = audioSrc.pitch + Random.Range(-pitchRandomizer, pitchRandomizer);
        audioSrc.Play();
        crackEffect.Play();
        sr.enabled = false;

        if (collision.TryGetComponent(out Health health))
        {
            health.TakeDamage(1);
        }
    }
}
