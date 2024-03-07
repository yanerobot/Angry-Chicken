using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject[] menus;

    void OnValidate()
    {
        menus = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; ++i)
        {
            menus[i] = gameObject.transform.GetChild(i).gameObject;
        }
    }
    public void Show(string name)
    {
        foreach (var obj in menus)
        {
            if (obj.name == name)
                obj.SetActive(true);
            else
                obj.SetActive(false);
        }
    }
    public void HideAll()
    {
        foreach (var obj in menus)
        {
            obj.SetActive(false);
        }
    }
}
