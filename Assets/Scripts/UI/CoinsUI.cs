using UnityEngine;
using TMPro;

public class CoinsUI : MonoBehaviour
{
    [SerializeField] CoinsCollector collector;
    [SerializeField] TMP_Text coinTextMesh;


    void OnEnable()
    {
        collector.OnPick += DisplayCurrentCoins;
    }

    void DisplayCurrentCoins(int totalCoins)
    {
        coinTextMesh.text = " x " + totalCoins;
    }
}
