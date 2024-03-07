using UnityEngine;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private GameObject[] heartObjs;

    public void Init()
    {
        DisplayCurrentHealth(health.maxHealth);
        health.OnDamage += DisplayCurrentHealth;
    }

    void DisplayCurrentHealth(int currentHealth)
    {
        print(currentHealth);

        for (int i = 0; i < heartObjs.Length; i++)
        {
            if (i + 1 > currentHealth)
            {
                heartObjs[i].SetActive(false);
            } 
            else
            {
                heartObjs[i].SetActive(true);
            }
        }
    }

    void AddHeart(int currentHealth)
    {

    }
}
