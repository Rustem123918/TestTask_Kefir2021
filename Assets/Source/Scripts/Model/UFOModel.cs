using UnityEngine;

public class UFOModel
{
    public Vector2 CurrentPosition => currentPosition;
    private Vector2 currentPosition;
    public float Rotation => rotation;
    private float rotation;
    private Vector2 shipPosition;
    private readonly float speed;

    private GameData gameData;
    
    public UFOModel(Vector2 startPosition, Vector2 shipPosition, float speed, GameData gameData)
    {
        this.currentPosition = startPosition;
        this.shipPosition = shipPosition;
        this.speed = speed;
        this.gameData = gameData;
    }
    public void Move()
    {
        LookAtShip();
        Vector2 direction = new Vector2(Mathf.Cos(rotation * Mathf.Deg2Rad), Mathf.Sin(rotation * Mathf.Deg2Rad));
        currentPosition = currentPosition + direction * speed * Time.fixedDeltaTime;
        CheckBorders();
    }
    private void LookAtShip()
    {
        var lookDir = shipPosition - currentPosition;
        var angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rotation = angle;
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
