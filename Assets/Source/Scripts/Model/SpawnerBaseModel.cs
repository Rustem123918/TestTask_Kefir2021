using System.Collections;
using UnityEngine;

public class SpawnerBaseModel
{
    public event System.Action<Vector2, float> OnSpawn;

    public static Vector2 GameZone => gameZone;
    private static Vector2 gameZone;

    private Vector2 timeDelayRange;
    private Vector2 objectsCountRange;
    private float timeDelay;

    private Coroutine spawnRoutine;

    static SpawnerBaseModel()
    {
        gameZone = GetGameZone();
    }
    public SpawnerBaseModel(SpawnerData spawnerData)
    {
        this.timeDelayRange = spawnerData.TimeDelayRange;
        this.objectsCountRange = spawnerData.ObjectsCountRange;
        this.timeDelay = spawnerData.TimeDelay;
    }
    /// <summary>
    /// Метод вычисляет размеры игровой зоны, исходя из размеров экрана пользователя
    /// </summary>
    /// <returns>Возвращает размер игровой зоны</returns>
    public static Vector2 GetGameZone()
    {
        var cam = Camera.main;
        var topRight = (Vector2)cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
        var bottomLeft = (Vector2)cam.ScreenToWorldPoint(Vector3.zero);
        var result = topRight - bottomLeft;

        return result;
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
    /// Возвращает случайную точку спавна на границе спавнера
    /// </summary>
    /// <returns>Точка спавна</returns>
    private Vector2 GetRandomPositon()
    {
        Vector2 spawnPosition = gameZone / 2f;
        var r = Random.Range(2, 4);

        // постоянный X и рандомный Y
        if (r % 2 == 0)
        {
            r = Random.Range(2, 4);
            // спавн на правой границе
            if (r % 2 == 0)
            {
                spawnPosition.x = gameZone.x / 2f;
            }
            // спавн на левой границе
            else
            {
                spawnPosition.x = -gameZone.x / 2f;
            }
            spawnPosition.y = Random.Range(-gameZone.y / 2f, gameZone.y / 2f);
        }
        // постоянный Y и рандомный X
        else
        {
            r = Random.Range(2, 4);
            if (r % 2 == 0)
            {
                // спавн на верхней границе
                spawnPosition.y = gameZone.y / 2f;
            }
            else
            {
                // спавн на нижней границе
                spawnPosition.y = -gameZone.y / 2f;
            }
            spawnPosition.x = Random.Range(-gameZone.x / 2f, gameZone.x / 2f);
        }
        return spawnPosition;
    }
}
