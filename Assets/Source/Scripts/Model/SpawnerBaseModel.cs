using System.Collections;
using UnityEngine;

public class SpawnerBaseModel
{
    public event System.Action<Vector2, float> OnSpawn;
    public event System.Action OnStartSpawnWithDelayRoutine;

    public static Vector2 GameZone => gameZone;
    private static Vector2 gameZone;

    private Vector2 timeDelayRange;
    private Vector2 objectsCountRange;
    private float timeDelay;

    static SpawnerBaseModel()
    {
        gameZone = GetGameZone();
    }
    public SpawnerBaseModel(Vector2 timeDelayRange, Vector2 objectsCountRange, float timeDealy)//SpawnerData spawnerData)
    {
        this.timeDelayRange = timeDelayRange;//spawnerData.TimeDelayRange;
        this.objectsCountRange = objectsCountRange;//spawnerData.ObjectsCountRange;
        this.timeDelay = timeDealy;// spawnerData.TimeDelay;
    }
    /// <summary>
    /// ����� ��������� ������� ������� ����, ������ �� �������� ������ ������������
    /// </summary>
    /// <returns>���������� ������ ������� ����</returns>
    public static Vector2 GetGameZone()
    {
        var cam = Camera.main;
        var topRight = (Vector2)cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
        var bottomLeft = (Vector2)cam.ScreenToWorldPoint(Vector3.zero);
        var result = topRight - bottomLeft;

        return result;
    }
    public IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeDelay);
            var count = GetCountAsteroidsToSpawn();
            for (int i = 0; i < count; i++)
                OnStartSpawnWithDelayRoutine?.Invoke();
        }
    }
    public IEnumerator SpawnWithDelayRoutine()
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
        Vector2 spawnPosition = gameZone / 2f;
        var r = Random.Range(2, 4);

        // ���������� X � ��������� Y
        if (r % 2 == 0)
        {
            r = Random.Range(2, 4);
            // ����� �� ������ �������
            if (r % 2 == 0)
            {
                spawnPosition.x = gameZone.x / 2f;
            }
            // ����� �� ����� �������
            else
            {
                spawnPosition.x = -gameZone.x / 2f;
            }
            spawnPosition.y = Random.Range(-gameZone.y / 2f, gameZone.y / 2f);
        }
        // ���������� Y � ��������� X
        else
        {
            r = Random.Range(2, 4);
            if (r % 2 == 0)
            {
                // ����� �� ������� �������
                spawnPosition.y = gameZone.y / 2f;
            }
            else
            {
                // ����� �� ������ �������
                spawnPosition.y = -gameZone.y / 2f;
            }
            spawnPosition.x = Random.Range(-gameZone.x / 2f, gameZone.x / 2f);
        }
        return spawnPosition;
    }
}
