using UnityEngine;

[CreateAssetMenu(fileName = "SpawnerData", menuName = "ScriptableObjects/SpawnerData", order = 2)]
public class SpawnerData : ScriptableObject
{
    public Vector2 TimeDelayRange;
    public Vector2 ObjectsCountRange;
    public float TimeDelay;
}
