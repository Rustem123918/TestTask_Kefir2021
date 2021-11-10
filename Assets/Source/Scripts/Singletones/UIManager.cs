using Supyrb;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    private Text coinsText;

    [SerializeField]
    private Text maxCharge;
    [SerializeField]
    private Text currentCharge;
    [SerializeField]
    private Text time;

    private LaserModel laserModel;
    private UpdateCoinsSignal<int> updateCoinsSignal;
    private void Awake()
    {
        Signals.Get(out updateCoinsSignal);
        updateCoinsSignal.AddListener(UpdateCoinsText);
    }
    private void Start()
    {
        laserModel = FindObjectOfType<Ship>().LaserModel;
        maxCharge.text = laserModel.MaxCharge.ToString();
    }
    private void Update()
    {
        currentCharge.text = laserModel.CurrentCharge.ToString("0.00");
        time.text = laserModel.TimeLeft.ToString("0.00");
    }
    private void UpdateCoinsText(int value)
    {
        coinsText.text = value.ToString();
    }
}
