using UnityEngine;

public class CanvasHandler : MonoBehaviour
{
    public GameObject[] UIElements;

    void OnValidate()
    {
        UIElements = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; ++i)
        {
            UIElements[i] = gameObject.transform.GetChild(i).gameObject;
        }
    }
    public void Show(string name)
    {
        transform.Find(name).gameObject.SetActive(true);
    }
    public void HideAll()
    {
        foreach (var UIBlock in UIElements)
        {
            UIBlock.SetActive(false);
        }
    }

    public void Hide(string name)
    {
        transform.Find(name).gameObject.SetActive(false);
    }

    public void ShowLoadingScreen(bool flag)
    {
        if (flag) Show("LoadingScreen");
        else Hide("LoadingScreen");
    }
    public void ShowGameOverScreen(bool flag)
    {
        if (flag) Show("GameOver");
        else Hide("GameOver");
    }
    public void ShowLevelCompletedScreeen(bool flag)
    {
        if (flag) Show("LevelCompleted");
        else Hide("LevelCompleted");
    }
    public void ShowGameplayUI()
    {
        HideAll();
        Show("HealthUI");
        Show("CoinsUI");
    }
}
