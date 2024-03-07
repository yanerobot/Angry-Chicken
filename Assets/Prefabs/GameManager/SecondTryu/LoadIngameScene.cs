public class LoadIngameScene : StateStatic
{
    GameManager GM;
    int sceneToLoad;
    public LoadIngameScene(StateMachineStatic stateMachine, int sceneToLoad) : base(stateMachine)
    {
        GM = stateMachine as GameManager;

        this.sceneToLoad = sceneToLoad;
    }

    public override void OnEnter()
    {
        if(!GM.IsSceneLoaded(sceneToLoad))
        {
            GM.StartCoroutine(GM.LoadScene(sceneToLoad));
        }
        else
        {
            GM.FindSceneHandler(sceneToLoad);
        }
        GM.currentSceneID = sceneToLoad;
        GM.SetState(new IngameState(GM));
    }
}
