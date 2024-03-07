using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    [HideInInspector] public Health health;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Health health))
        {
            this.health = health;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Health _))
        {
            health = null;
        }
    }

}
