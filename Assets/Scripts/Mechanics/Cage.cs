using UnityEngine;

public class Cage : Switcher
{
    [SerializeField] Animator animator;

    public override void Activate()
    {
        animator.enabled = true;
        Destroy(gameObject);
    }
}
