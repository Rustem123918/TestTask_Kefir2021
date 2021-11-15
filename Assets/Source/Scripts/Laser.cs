using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private LaserData laserData;

    [SerializeField]
    private Transform firePosition;
    [SerializeField]
    private GameObject laserPrefab;

    private GameObject laser;

    public LaserModel LaserModel => laserModel;
    private LaserModel laserModel;

    private Coroutine increaseChargeRoutine;
    public void Fire()
    {
        laserModel.Fire();
    }
    public void StopFire()
    {
        if (laser != null)
            Destroy(laser);
        laser = null;
    }
    private void Awake()
    {
        laserModel = new LaserModel(laserData.MaxCharge, laserData.DecreaseChargeSpeed, laserData.IncreaseChargeSpeed, laserData.RegenerateTimeDelay);
        laserModel.OnFire += FireLaser;
        laserModel.OnChargeEnd += StopFire;
        laserModel.OnStopIncreaseChargeRoutine += () =>
        {
            StopCoroutine(increaseChargeRoutine);
            increaseChargeRoutine = null;
        };
        laserModel.OnStartIncreaseChargeRoutine += () =>
        {
            increaseChargeRoutine = StartCoroutine(laserModel.IncreaseChargeRoutine());
        };
        StartCoroutine(laserModel.RegenerationRoutine());
    }
    private void FireLaser()
    {
        if (laser == null)
        {
            laser = Instantiate(laserPrefab, firePosition.position, firePosition.rotation);
        }
        else
        {
            laser.transform.position = firePosition.position;
            laser.transform.rotation = firePosition.rotation;
        }
    }
}
