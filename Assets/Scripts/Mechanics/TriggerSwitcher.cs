using UnityEngine;

public class TriggerSwitcher : Switcher
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(out PlayerMain _))
            return;

        Activate();
    }
}
