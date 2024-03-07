using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DamageDealOnEvent : MonoBehaviour
{
    [SerializeField, Range(0,100)] private int damagePercentPerHit;

    List<Health> currentTargets;

    void Awake()
    {
        currentTargets = new List<Health>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Health health))
        {
            currentTargets.Add(health);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Health health))
        {
            if (currentTargets.Contains(health))
            {
                currentTargets.Remove(health);
            }
        }
    }
    void DealDamage()
    {

        foreach(var target in currentTargets)
        {
            print(((float)damagePercentPerHit / 100f));
            print(target.maxHealth * ((float)damagePercentPerHit / 100));
            target.TakeDamage((int)(target.maxHealth * ((float)damagePercentPerHit / 100)));
        }
    }
}
