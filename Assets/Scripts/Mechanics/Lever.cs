using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class Lever : Switcher
{
    [SerializeField] private Sprite openSprite;
    [SerializeField] private Sprite closeSprite;

    SpriteRenderer currentSprite;

    void Awake()
    {
        currentSprite = GetComponent<SpriteRenderer>();
    }

    public override void Activate()
    {
        base.Activate();

        if (isActivated)
            currentSprite.sprite = openSprite;
        else
            currentSprite.sprite = closeSprite;
    }
}
