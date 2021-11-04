using UnityEngine;

public class UFOSpawner : MonoBehaviour
{
    [SerializeField]
    private GameData gameData;
    [SerializeField]
    private UFOSpawnerData spawnerData;
    [SerializeField]
    private GameObject ufoPrefab;

    private UFOSpawnerModel model;
    private void Awake()
    {
        model = new UFOSpawnerModel(spawnerData, gameData);
        model.OnSpawn += Spawn;
    }
    private void Start()
    {
        model.StartSpawning();
    }
    private void Spawn(Vector2 pos)
    {
        Instantiate(ufoPrefab, pos, Quaternion.identity);
    }

    private void OnDrawGizmos()
    {
        if (gameData != null)
            Gizmos.DrawWireCube(Vector3.zero, gameData.GameZone);
    }
}
