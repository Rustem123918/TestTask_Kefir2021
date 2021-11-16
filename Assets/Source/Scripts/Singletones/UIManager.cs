using Supyrb;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    private GameObject inGameUI;
    [SerializeField]
    private GameObject startUI;
    [SerializeField]
    private GameObject endUI;

    [Header("Coins")]
    [SerializeField]
    private Text coinsTextInGame;
    [SerializeField]
    private Text coinsTextEnd;

    [Header("Laser data")]
    [SerializeField]
    private Text maxCharge;
    [SerializeField]
    private Text currentCharge;
    [SerializeField]
    private Text timeLeft;

    [Header("Ship data")]
    [SerializeField]
    private Text currentPosition;
    [SerializeField]
    private Text currentRotation;
    [SerializeField]
    private Text currentVeocity;

    private ShipModel shipModel;
    private LaserModel laserModel;

    private UpdateCoinsSignal<int> updateCoinsSignal;
    private GameStartSignal gameStartSignal;
    private GameOverSignal gameOverSignal;

    private bool gameIsOn;

    private void Awake()
    {
        Signals.Get(out updateCoinsSignal);
        Signals.Get(out gameStartSignal);
        Signals.Get(out gameOverSignal);

        updateCoinsSignal.AddListener(UpdateCoinsText);
        gameStartSignal.AddListener(GameStartSignalHandler);
        gameOverSignal.AddListener(ShowEndUI);

        ShowUI(startUI);
        gameIsOn = false;
    }
    private void Update()
    {
        if (!gameIsOn)
            return;
        UpdateLaserUI(laserModel.CurrentCharge, laserModel.TimeLeft);
        UpdateShipUI(shipModel.CurrentPosition, shipModel.Rotation, shipModel.Velocity);
    }
    private void GameStartSignalHandler()
    {
        laserModel = FindObjectOfType<Laser>().LaserModel;
        shipModel = FindObjectOfType<Ship>().ShipModel;

        maxCharge.text = laserModel.MaxCharge.ToString();

        ShowInGameUI();

        gameIsOn = true;
    }
    private void UpdateCoinsText(int value)
    {
        coinsTextInGame.text = value.ToString();
        coinsTextEnd.text = "Score: " + value.ToString();
    }
    private void UpdateLaserUI(float charge, float time)
    {
        currentCharge.text = charge.ToString("0.00");
        timeLeft.text = time.ToString("0.00");
    }
    private void UpdateShipUI(Vector2 position, float rotation, float velocity)
    {
        currentPosition.text = position.ToString();
        currentRotation.text = rotation.ToString("0.00");
        currentVeocity.text = velocity.ToString("0.00");
    }
    private void ShowInGameUI() => ShowUI(inGameUI);
    private void ShowEndUI() => ShowUI(endUI);
    private void ShowUI(GameObject uiToShow)
    {
        GameObject[] allUI = { inGameUI, startUI, endUI};
        foreach (var uiToHide in allUI)
            uiToHide.SetActive(false);

        uiToShow.SetActive(true);
    }
}
