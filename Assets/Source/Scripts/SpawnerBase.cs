using UnityEngine;

public class SpawnerBase : MonoBehaviour
{
    [SerializeField]
    private SpawnerData spawnerData;
    [SerializeField]
    private GameObject objectToSpawnPrefab;

    private SpawnerBaseModel model;
    private void Awake()
    {
        model = new SpawnerBaseModel(spawnerData);
        model.OnSpawn += Spawn;
    }
    private void Start()
    {
        model.StartSpawning();
    }
    private void Spawn(Vector2 pos, float rot)
    {
        var rotation = Quaternion.Euler(0f, 0f, rot);
        Instantiate(objectToSpawnPrefab, pos, rotation);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(Vector3.zero, SpawnerBaseModel.GameZone);
    }
}
