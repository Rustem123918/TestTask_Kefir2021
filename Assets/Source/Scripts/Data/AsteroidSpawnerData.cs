using UnityEngine;

[CreateAssetMenu(fileName = "AsteroidSpawnerData", menuName = "ScriptableObjects/AsteroidSpawnerData", order = 2)]
public class AsteroidSpawnerData : ScriptableObject
{
    public Vector2 TimeDelayRange;
    public Vector2 AsteroidCountRange;
    public float TimeDelay;
}
