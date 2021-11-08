using Supyrb;

public class CoinsManager : Singleton<CoinsManager>
{
    private int coinsCount;

    private AddCoinsSignal<int> addCoinsSignal;
    private UpdateCoinsSignal<int> updateCoinsSignal;
    private void Awake()
    {
        coinsCount = 0;

        Signals.Get(out addCoinsSignal);
        addCoinsSignal.AddListener(AddCoins);

        Signals.Get(out updateCoinsSignal);
    }
    public void AddCoins(int value)
    {
        coinsCount += value;
        updateCoinsSignal.Dispatch(coinsCount);
    }
}
