using System.Collections;
using UnityEngine;

public class AsteroidSpawnerModel
{
    public event System.Action<Vector2> OnSpawn;

    private Vector2 timeDelayRange;
    private Vector2 asteroidCountRange;
    private float timeDelay;
    private GameData gameData;

    private Coroutine spawnRoutine;

    public AsteroidSpawnerModel(Vector2 timeDelayRange, Vector2 asteroidCountRange, float timeDelay, GameData gameData)
    {
        this.timeDelayRange = timeDelayRange;
        this.asteroidCountRange = asteroidCountRange;
        this.timeDelay = timeDelay;
        this.gameData = gameData;
    }
    public void StartSpawning()
    {
        spawnRoutine = CoroutineManager.Instance.StartRoutine(SpawnRoutine());
    }
    public void StopSpawnin()
    {
        CoroutineManager.Instance.StopRoutine(spawnRoutine);
    }
    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeDelay);
            var count = GetCountAsteroidsToSpawn();
            for (int i = 0; i < count; i++)
                CoroutineManager.Instance.StartRoutine(SpawnWithDelay());
        }
    }
    private IEnumerator SpawnWithDelay()
    {
        var delay = GetTimeDelay();
        yield return new WaitForSeconds(delay);
        var pos = GetRandomPositon();
        OnSpawn?.Invoke(pos);
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
    /// ���������� ��������� ����� ������ �� ������� ��������
    /// </summary>
    /// <returns>����� ������</returns>
    private Vector2 GetRandomPositon()
    {
        Vector2 spawnPosition = gameData.GameZone / 2f;
        var r = Random.Range(2, 4);

        // ���������� X � ��������� Y
        if (r % 2 == 0)
        {
            r = Random.Range(2, 4);
            // ����� �� ������ �������
            if (r % 2 == 0)
            {
                spawnPosition.x = gameData.GameZone.x / 2f;
            }
            // ����� �� ����� �������
            else
            {
                spawnPosition.x = -gameData.GameZone.x / 2f;
            }
            spawnPosition.y = Random.Range(-gameData.GameZone.y / 2f, gameData.GameZone.y / 2f);
        }
        // ���������� Y � ��������� X
        else
        {
            r = Random.Range(2, 4);
            if (r % 2 == 0)
            {
                // ����� �� ������� �������
                spawnPosition.y = gameData.GameZone.y / 2f;
            }
            else
            {
                // ����� �� ������ �������
                spawnPosition.y = -gameData.GameZone.y / 2f;
            }
            spawnPosition.x = Random.Range(-gameData.GameZone.x / 2f, gameData.GameZone.x / 2f);
        }
        return spawnPosition;
    }
}
