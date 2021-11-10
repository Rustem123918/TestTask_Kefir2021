using UnityEngine;

public class Pistol : MonoBehaviour
{
    [SerializeField]
    private PistolData pistolData;

    [SerializeField]
    private Transform firePosition;
    [SerializeField]
    private GameObject bulletPrefab;

    private PistolModel pistolModel;
    public void Fire()
    {
        pistolModel.Fire();
    }
    private void Awake()
    {
        pistolModel = new PistolModel(pistolData.FireDelay);
        pistolModel.OnFire += FireBullet;
    }
    private void FireBullet()
    {
        Instantiate(bulletPrefab, firePosition.position, firePosition.rotation);
    }
}
