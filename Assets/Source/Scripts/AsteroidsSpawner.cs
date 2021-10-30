using System.Collections;
using UnityEngine;

public class AsteroidsSpawner : MonoBehaviour
{
    public Vector2 SpawnZone => spawnZone;
    [SerializeField]
    private Vector2 spawnZone;
    [SerializeField]
    private Vector2 timeDelayRange;
    [SerializeField]
    private Vector2 asteroidCountRange;
    [SerializeField]
    private float timeDelay;

    [SerializeField]
    private GameObject asteroidPrefab;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }
    private void Spawn(Vector2 pos)
    {
        Instantiate(asteroidPrefab, pos, Quaternion.identity);
        Debug.Log("Spawn asteroid in position: " + pos.ToString());
    }
    private IEnumerator SpawnRoutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(timeDelay);
            var count = GetCountAsteroidsToSpawn();
            for (int i = 0; i < count; i++)
                StartCoroutine(SpawnWithDelay());
        }
    }
    private IEnumerator SpawnWithDelay()
    {
        var delay = GetTimeDelay();
        yield return new WaitForSeconds(delay);
        var pos = GetRandomPositon();
        Spawn(pos);
    }
    private float GetTimeDelay()
    {
        return Random.Range(timeDelayRange.x, timeDelayRange.y);
    }
    private int GetCountAsteroidsToSpawn()
    {
        return Random.Range((int)asteroidCountRange.x, (int)asteroidCountRange.y);
    }
    /// <summary>
    /// Возвращает случайную точку спавна на границе спавнера
    /// </summary>
    /// <returns>Точка спавна</returns>
    private Vector2 GetRandomPositon()
    {
        Vector2 spawnPosition = spawnZone/2f;
        var r = Random.Range(2, 4);

        // постоянный X и рандомный Y
        if(r % 2 == 0)
        {
            r = Random.Range(2, 4);
            // спавн на правой границе
            if(r % 2 == 0)
            {
                spawnPosition.x = spawnZone.x / 2f;
            }
            // спавн на левой границе
            else
            {
                spawnPosition.x = -spawnZone.x / 2f;
            }
            spawnPosition.y = Random.Range(-spawnZone.y / 2f, spawnZone.y / 2f);
        }
        // постоянный Y и рандомный X
        else
        {
            r = Random.Range(2, 4);
            if (r % 2 == 0)
            {
                // спавн на верхней границе
                spawnPosition.y = spawnZone.y / 2f;
            }
            else
            {
                // спавн на нижней границе
                spawnPosition.y = -spawnZone.y / 2f;
            }
            spawnPosition.x = Random.Range(-spawnZone.x / 2f, spawnZone.x / 2f);
        }
        return spawnPosition;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(Vector3.zero, spawnZone);
    }
}
