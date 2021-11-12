using Supyrb;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    private Text coinsText;

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
    private void Awake()
    {
        Signals.Get(out updateCoinsSignal);
        updateCoinsSignal.AddListener(UpdateCoinsText);
    }
    private void Start()
    {
        laserModel = FindObjectOfType<Laser>().LaserModel;
        shipModel = FindObjectOfType<Ship>().ShipModel;

        maxCharge.text = laserModel.MaxCharge.ToString();
    }
    private void Update()
    {
        UpdateLaserUI(laserModel.CurrentCharge, laserModel.TimeLeft);
        UpdateShipUI(shipModel.CurrentPosition, shipModel.Rotation, shipModel.Velocity);
    }
    private void UpdateCoinsText(int value)
    {
        coinsText.text = value.ToString();
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
}
