using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOSpawnerModel
{
    public event System.Action<Vector2> OnSpawn;

    private Vector2 timeDelayRange;
    private Vector2 ufoCountRange;
    private float timeDelay;
    private GameData gameData;

    private Coroutine spawnRoutine;

    public UFOSpawnerModel(Vector2 timeDelayRange, Vector2 ufoCountRange, float timeDelay, GameData gameData)
    {
        this.timeDelayRange = timeDelayRange;
        this.ufoCountRange = ufoCountRange;
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
        return Random.Range((int)ufoCountRange.x, (int)ufoCountRange.y);
    }
    /// <summary>
    /// Возвращает случайную точку спавна на границе спавнера
    /// </summary>
    /// <returns>Точка спавна</returns>
    private Vector2 GetRandomPositon()
    {
        Vector2 spawnPosition = gameData.GameZone / 2f;
        var r = Random.Range(2, 4);

        // постоянный X и рандомный Y
        if (r % 2 == 0)
        {
            r = Random.Range(2, 4);
            // спавн на правой границе
            if (r % 2 == 0)
            {
                spawnPosition.x = gameData.GameZone.x / 2f;
            }
            // спавн на левой границе
            else
            {
                spawnPosition.x = -gameData.GameZone.x / 2f;
            }
            spawnPosition.y = Random.Range(-gameData.GameZone.y / 2f, gameData.GameZone.y / 2f);
        }
        // постоянный Y и рандомный X
        else
        {
            r = Random.Range(2, 4);
            if (r % 2 == 0)
            {
                // спавн на верхней границе
                spawnPosition.y = gameData.GameZone.y / 2f;
            }
            else
            {
                // спавн на нижней границе
                spawnPosition.y = -gameData.GameZone.y / 2f;
            }
            spawnPosition.x = Random.Range(-gameData.GameZone.x / 2f, gameData.GameZone.x / 2f);
        }
        return spawnPosition;
    }
}
