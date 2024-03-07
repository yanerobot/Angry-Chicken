using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] IngameHandler sceneHandler;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerMain player))
        {
            sceneHandler.LevelCompleted();
        }
    }
}
