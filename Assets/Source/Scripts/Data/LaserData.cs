using UnityEngine;

[CreateAssetMenu(fileName = "LaserData", menuName = "ScriptableObjects/LaserData", order = 3)]
public class LaserData : ScriptableObject
{
    public float MaxCharge;
    public float DecreaseChargeSpeed;
    public float IncreaseChargeSpeed;
    public float RegenerateTimeDelay;
}
