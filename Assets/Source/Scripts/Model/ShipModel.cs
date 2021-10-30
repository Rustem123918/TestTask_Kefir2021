using UnityEngine;

public class ShipModel
{
    public float Velocity => (currentPosition - previousPosition).magnitude / Time.fixedDeltaTime;

    private readonly float speed;

    public Vector2 CurrentPosition => currentPosition;
    private Vector2 currentPosition;

    private Vector2 previousPosition;

    public float Rotation => rotation;
    private float rotation;

    private GameData gameData;
    public ShipModel(float speed, GameData gameData)
    {
        this.speed = speed;
        currentPosition = Vector2.zero;
        previousPosition = Vector2.zero;
        rotation = 0f;

        this.gameData = gameData;
    }
    public void Move(Vector2 direction)
    {
        previousPosition = currentPosition;
        currentPosition = previousPosition + direction * speed * Time.fixedDeltaTime;
        CheckBorders();
    }
    public void Rotate(float angle)
    {
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
