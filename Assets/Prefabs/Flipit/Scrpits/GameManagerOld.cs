using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManagerOld : MonoBehaviour
{
    [Header("Main")]
//   [SerializeField] ScenesSO scenesSO;
 //   [SerializeField] PointsSO pointsSO;

    [Header("In game")]
    [SerializeField] GameObject SettingsMenu;

    [Header("Audio")]
    [SerializeField] AudioSource levelCompletedSource;
    [SerializeField] AudioSource mainMusicSource;

    
    public static int points { get; private set; }  = 0;
    
    
    public static GameManagerOld gameManager { get; private set; }

    public static PlayerMain  _playerScript { get; private set; }
    public static readonly int _playerLayerNum = 3;
    public static readonly int _juiceLayerNum = 6;
    public static readonly int _playerGFXlayerNum = 8;


    static bool loading = false;
    static Animator animator;

    Coroutine pointSubtruction = null;

    private void Awake()
    {
        if(gameManager == null)
            gameManager = this;
        animator = FindObjectOfType<Canvas>().GetComponent<Animator>();
    }

    private void Start()
    {
        //      if (PlayerMa.player != null)
        //     ResetParams();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            ToggleSettingsMenu();
        

        if (Input.GetKeyDown(KeyCode.Y))
        {
            //       pointsSO.currentScore++;
        }

    }

    void ToggleSettingsMenu()
    {
        bool enabled = SettingsMenu.activeInHierarchy ? false : true;

        SettingsMenu.SetActive(enabled);
        Pause(enabled);
    }

    

    public static void OnLevelWasLoaded(int level)
    {
        points = 0;
    }


    void Init()
    {
        if (pointSubtruction == null)
            pointSubtruction = StartCoroutine(SubtractPoints(1));
    }

    static IEnumerator LoadLevel(int levelToLoad)
    {
        if (loading) yield break;

        SetLoadingParams();

        yield return new WaitForSeconds(0.3f); // for animation

        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
        AsyncOperation progress = SceneManager.LoadSceneAsync(levelToLoad, LoadSceneMode.Additive);

        while (!progress.isDone) yield return null;
        
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(levelToLoad));
        ResetParams();
        gameManager.Init();
    }

    private static void ResetParams()
    {
        //    Player.player.ResetPlayer();
        animator.SetBool("GameOver", false);
        animator.SetBool("LevelCompleted", false);
        loading = false;
        animator.SetBool("Loading", false);
    }

    private static void SetLoadingParams()
    {
        //       Player.Freeze(true);
        loading = true;
        animator.SetBool("Loading", true);
    }

    public void Pause(bool enabled)
    {
        Time.timeScale = enabled ? 0 : 1;
    }

    
    IEnumerator SubtractPoints(float seconds)
    {
        var waitforseconds = new WaitForSeconds(seconds);
        while(true)
        {
            points -= 2;
            if (points < 0) points = 0;
            //       Score.SetPoints(points.ToString());
            yield return waitforseconds;
        }
    }
    public void StopPointsSubtruction()
    {
        CancelInvoke();
    }

    public static void RestartLevel()
    {
        gameManager.StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
    }

    private static bool IsLastScene(int scene)
    {
        int lastScene = SceneManager.sceneCountInBuildSettings - 2;

        if (scene == lastScene)
        {
            return true;
        }
        return false;
    }

    public static void LoadNextLevel()
    {
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;

        if (IsLastScene(nextScene))
        {
            LoadCredits();
            return;
        }

        gameManager.StartCoroutine(LoadLevel(nextScene));
    }

    public static void LoadCredits()
    {
        //      SceneManager.LoadScene(gameManager.scenesSO.creditsSceneName);
    }

    public static void NewGame(int level)
    {
        AsyncOperation lvlLoad = SceneManager.LoadSceneAsync(level, LoadSceneMode.Additive);
        lvlLoad.completed += (op) =>
        {
            //           SceneManager.UnloadSceneAsync(gameManager.scenesSO.menuSceneName);
            //          AsyncOperation gamePlayScene = SceneManager.LoadSceneAsync(gameManager.scenesSO.gameplaySceneName, LoadSceneMode.Additive);
            //          gamePlayScene.completed += (op) => gameManager.Init();
        };
    }

    public static void AddPoints(int p)
    {
        points += p;
        //    Score.SetPoints(points.ToString());

    }

    public static void GameOver()
    {
        animator.SetBool("GameOver", true);
    }

    public static void LevelCompleted()
    {
        gameManager.levelCompletedSource.Play();
        animator.SetBool("LevelCompleted", true);
    }

    
    public static void GoToMainMenu()
    {
        //        SceneManager.LoadScene(gameManager.scenesSO.menuSceneName);
    }

    public static void ExitApp()
    {
        Application.Quit();
    }
}
