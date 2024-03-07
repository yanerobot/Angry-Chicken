using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class LevelLoader : MonoBehaviour
{
    [Header("IMPORTANT! Set current scene:")]
    public int currentSceneIndex = -1;
    [Space]
    public float additionalLoadingTime = 2f;
    
    private CanvasHandler canvasHandler;

    void Start()
    {
        canvasHandler = FindObjectOfType<CanvasHandler>();
        if (canvasHandler == null)
        {
            throw new System.ArgumentNullException("Couldn't find canvasHandler");
        }
        if (currentSceneIndex == -1)
        {
            currentSceneIndex = 0;
            throw new System.MissingFieldException("Scene is not set! First scene will be loaded");
        }
    }

    IEnumerator LoadSceneAsyncWithLoadingScreen(int newIndex)
    {
        DontDestroyOnLoad(gameObject);
        canvasHandler.Show("LoadingScreen");

        var isLoaded = IsSceneLoaded(newIndex);

        if (!isLoaded)
        {
            var load = SceneManager.LoadSceneAsync(newIndex);

            while (load.progress < 0.9f)
            {
                yield return null;
            }

            yield return new WaitForSeconds(additionalLoadingTime);


            var ob = SceneManager.GetSceneByBuildIndex(newIndex).GetRootGameObjects();

            foreach(var obj in ob)
            {
                if (obj.TryGetComponent(out LevelLoader _))
                    Destroy(obj);
            }
        }

        Debug.Log($"Scene: \"{newIndex}\" was successfully loaded.");

        
    }

    bool IsSceneLoaded(int buildIndex)
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            var sceneToLoad = SceneManager.GetSceneByBuildIndex(buildIndex);

            if (SceneManager.GetSceneAt(i) == sceneToLoad)
            {
                Debug.LogWarning($"Scene {buildIndex} is already loaded");
                return true;
            }
        }
        return false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartScene()
    {
        StartCoroutine(LoadSceneAsyncWithLoadingScreen(currentSceneIndex));
    }
    public void LoadNextScene()
    {
        StartCoroutine(LoadSceneAsyncWithLoadingScreen(currentSceneIndex + 1));
    }
    public void LoadScene(int index)
    {
        StartCoroutine(LoadSceneAsyncWithLoadingScreen(index));
    }
}
