using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Switcher switcher;
    [SerializeField] private Transform GFX;
    [SerializeField] private float openHeight;
    [SerializeField] private float closedHeight;


    void Awake()
    {
        switcher.OnActivate += OpenClose;
    }

    void OpenClose(bool isActivated)
    {
        if (isActivated)
            GFX.position = GFX.position.WhereY(openHeight);
        else
            GFX.position = GFX.position.WhereY(closedHeight);
    }
}
