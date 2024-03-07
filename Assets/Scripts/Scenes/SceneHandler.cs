using UnityEngine;

public abstract class SceneHandler : MonoBehaviour
{
    protected GameManager gameManager;

    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }
}
