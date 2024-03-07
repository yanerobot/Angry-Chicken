using UnityEngine;

public class CoinsCollector : MonoBehaviour
{
    int currentCoins = 0;

    internal delegate void Pick(int coins);
    internal event Pick OnPick;

    internal void Init()
    {
        currentCoins = 0;
        OnPick?.Invoke(currentCoins);
    }

    internal void PickCoin()
    {
        currentCoins += 1;
        OnPick?.Invoke(currentCoins);
    }
}
