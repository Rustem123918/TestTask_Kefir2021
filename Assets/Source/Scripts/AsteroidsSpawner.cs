using System.Collections;
using UnityEngine;

public class AsteroidsSpawner : MonoBehaviour
{
    public Vector2 SpawnZone => gameData.GameZone;
    [SerializeField]
    private GameData gameData;
    [SerializeField]
    private AsteroidSpawnerData spawnerData;
    [SerializeField]
    private GameObject asteroidPrefab;

    private AsteroidSpawnerModel model;
    private void Awake()
    {
        model = new AsteroidSpawnerModel(spawnerData, gameData);
        model.OnSpawn += Spawn;
    }
    private void Start()
    {
        model.StartSpawning();
    }
    private void Spawn(Vector2 pos, float rot)
    {
        var rotation = Quaternion.Euler(0f, 0f, rot);
        Instantiate(asteroidPrefab, pos, rotation);
    }
    
    private void OnDrawGizmos()
    {
        if(gameData != null)
            Gizmos.DrawWireCube(Vector3.zero, gameData.GameZone);
    }
}
