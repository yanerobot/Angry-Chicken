using UnityEngine.SceneManagement;
public class LoadStaticScene : StateStatic
{
    GameManager GM;
    int sceneToLoad;
    public LoadStaticScene(StateMachineStatic stateMachine, int sceneToLoad) : base(stateMachine)
    {
        GM = stateMachine as GameManager;
        this.sceneToLoad = sceneToLoad;
    }

    public override void OnEnter()
    {
        if (!GM.IsSceneLoaded(sceneToLoad)) 
            GM.StartCoroutine(GM.LoadScene(sceneToLoad));
        else
            GM.FindSceneHandler(sceneToLoad);

        GM.currentSceneID = sceneToLoad;
    }

    public override void OnExit()
    {
        GM.UnloadScene(GM.currentSceneID);
    }
}
