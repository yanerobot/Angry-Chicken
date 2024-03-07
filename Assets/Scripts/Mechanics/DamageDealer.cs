using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] bool oneShotKill;


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Health health))
        {
            if (oneShotKill)
                damage = health.maxHealth;
            health.TakeDamage(damage);
        }
    }
}
