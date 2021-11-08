using Supyrb;
using UnityEngine;

public class AddCoinsComponent : MonoBehaviour
{
    [SerializeField]
    private int coinsCount;

    private AddCoinsSignal<int> addCoinSignal;
    private void Awake()
    {
        Signals.Get(out addCoinSignal);
    }
    public void AddCoins()
    {
        addCoinSignal.Dispatch(coinsCount);
    }
}
