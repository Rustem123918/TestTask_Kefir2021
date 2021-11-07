using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBaseModel
{
    public event System.Action<Vector2, float> OnSpawn;

    private Vector2 timeDelayRange;
    private Vector2 objectsCountRange;
    private float timeDelay;
    private GameData gameData;

    private Coroutine spawnRoutine;

    public SpawnerBaseModel(SpawnerData spawnerData, GameData gameData)
    {
        this.timeDelayRange = spawnerData.TimeDelayRange;
        this.objectsCountRange = spawnerData.ObjectsCountRange;
        this.timeDelay = spawnerData.TimeDelay;
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
        var rot = GetRandomRotation();
        OnSpawn?.Invoke(pos, rot);
    }
    private float GetTimeDelay()
    {
        return Random.Range(timeDelayRange.x, timeDelayRange.y);
    }
    private int GetCountAsteroidsToSpawn()
    {
        return Random.Range((int)objectsCountRange.x, (int)objectsCountRange.y);
    }
    private float GetRandomRotation()
    {
        var r = Random.Range(2, 4);
        if (r % 2 == 0)
        {
            return Random.Range(1f, 179f);
        }
        else
        {
            return Random.Range(-1f, -179f);
        }
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
