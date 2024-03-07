using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
internal class IngameState : StateStatic
{
    GameManager GM;
    public IngameState(StateMachineStatic stateMachine) : base(stateMachine)
    {
        GM = stateMachine as GameManager;
    }

    public override void OnEnter()
    {
        GM.gameplay.SetActive(true);
        GM.player.position = (GM.sceneHandler as IngameHandler).PlayerInitialPosition;
        GM.canvasHandler.ShowGameplayUI();
    }

    public override void OnExit()
    {
        GM.gameplay.SetActive(false);
        var unload = SceneManager.UnloadSceneAsync(GM.currentSceneID);
        GM.StartCoroutine(HasUnloaded(unload));
    }

    IEnumerator HasUnloaded(AsyncOperation op)
    {
        canExit = false;
        yield return new WaitUntil(() => op.isDone);
        canExit = true;
    }
}