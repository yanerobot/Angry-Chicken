using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth;
    int currentHealth;

    internal bool isDead;

    public delegate void DieDelegate();
    public event DieDelegate OnDie;
    public delegate void Damage(int health);
    public event Damage OnDamage;

    public void Init()
    {
        currentHealth = maxHealth;
        isDead = false;
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        currentHealth -= amount;

        if (currentHealth < 0) currentHealth = 0;

        OnDamage?.Invoke(currentHealth);

        if (currentHealth == 0)
        {
            OnDie?.Invoke();
            isDead = true;
        }
    }
}

