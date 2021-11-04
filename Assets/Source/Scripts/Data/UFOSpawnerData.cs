using UnityEngine;

[CreateAssetMenu(fileName = "UFOSpawnerData", menuName = "ScriptableObjects/UFOSpawnerData", order = 7)]
public class UFOSpawnerData : ScriptableObject
{
    public Vector2 TimeDelayRange;
    public Vector2 UFOCountRange;
    public float TimeDelay;
}
