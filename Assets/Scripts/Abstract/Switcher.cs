using UnityEngine;
public abstract class Switcher : MonoBehaviour
{
    protected bool isActivated;

    public bool IsActivated
    {
        get { return isActivated; }
    }

    public delegate void Activator(bool isActivated);
    public event Activator OnActivate;
    public virtual void Activate()
    {
        isActivated = !isActivated;
        OnActivate?.Invoke(isActivated);
    }
}
