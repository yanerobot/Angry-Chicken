using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameHandler : SceneHandler
{
    [SerializeField] Transform levelStartingPoint;
    public Vector2 PlayerInitialPosition => levelStartingPoint.position;

    public void LoadNextScene()
    {
        gameManager.LoadNextScene();
    }

    public void RestartScene()
    {
        gameManager.RestartScene();
    }

    public void ToMenu()
    {
        gameManager.LoadScene((int)StaticScenes.MAIN_MENU);
    }

    public void GameOver()
    {
        gameManager.canvasHandler.ShowGameOverScreen(true);
    }
    public void LevelCompleted()
    {
        gameManager.canvasHandler.ShowLevelCompletedScreeen(true);
    }
}
