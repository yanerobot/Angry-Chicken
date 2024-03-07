using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] AudioSource src;
    SpriteRenderer rend;
    Animator animator;
    Collider2D coll;

    void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out CoinsCollector collector))
        {
            collector.PickCoin();
            animator.enabled = false;
            rend.enabled = false;
            coll.enabled = false;
            src.Play();
            Invoke(nameof(DestroySelf), src.clip.length);
        }
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
