using Supyrb;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    private Text coinsText;

    private UpdateCoinsSignal<int> updateCoinsSignal;
    private void Awake()
    {
        Signals.Get(out updateCoinsSignal);
        updateCoinsSignal.AddListener(UpdateCoinsText);
    }
    private void UpdateCoinsText(int value)
    {
        coinsText.text = value.ToString();
    }
}
