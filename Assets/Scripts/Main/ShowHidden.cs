using UnityEngine;

public class ShowHidden : MonoBehaviour
{
    [SerializeField] private GameObject obj;

    void Awake()
    {
        if (obj == null) throw new System.NullReferenceException("Object not set!");

        obj.gameObject.SetActive(true);
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        obj.gameObject.SetActive(false);
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        obj.gameObject.SetActive(true);
    }
}
