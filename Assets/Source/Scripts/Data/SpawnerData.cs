using UnityEngine;

[CreateAssetMenu(fileName = "SpawnerData", menuName = "ScriptableObjects/SpawnerData", order = 1)]
public class SpawnerData : ScriptableObject
{
    public Vector2 TimeDelayRange;
    public Vector2 ObjectsCountRange;
    public float TimeDelay;
}
