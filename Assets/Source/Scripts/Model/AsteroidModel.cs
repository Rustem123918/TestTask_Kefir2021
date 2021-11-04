using UnityEngine;

public class AsteroidModel
{
    public Vector2 CurrentPosition => currentPosition;
    private Vector2 currentPosition;
    public float Rotation => rotation;
    private float rotation;
    private readonly float speed;

    private GameData gameData;
    public AsteroidModel(Vector2 startPosition, float startRotation, float speed, GameData gameData)
    {
        this.currentPosition = startPosition;
        this.rotation = startRotation;
        this.speed = speed;
        this.gameData = gameData;
    }
    public void Move()
    {
        Vector2 direction = new Vector2(Mathf.Cos(rotation * Mathf.Deg2Rad), Mathf.Sin(rotation * Mathf.Deg2Rad));
        currentPosition = currentPosition + direction * speed * Time.fixedDeltaTime;
        CheckBorders();
    }
    private void CheckBorders()
    {
        var pos = currentPosition;

        if (pos.x > gameData.GameZone.x / 2f)
            pos.x = -gameData.GameZone.x / 2f;
        else if (pos.x < -gameData.GameZone.x / 2f)
            pos.x = gameData.GameZone.x / 2f;

        if (pos.y > gameData.GameZone.y / 2f)
            pos.y = -gameData.GameZone.y / 2f;
        else if (pos.y < -gameData.GameZone.y / 2f)
            pos.y = gameData.GameZone.y / 2f;

        currentPosition = pos;
    }
}
