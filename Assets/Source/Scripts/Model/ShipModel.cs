using UnityEngine;

public class ShipModel
{
    public float Velocity => (currentPosition - previousPosition).magnitude / Time.fixedDeltaTime;

    public Vector2 CurrentPosition => currentPosition;
    private Vector2 currentPosition;

    private Vector2 previousPosition;

    public float Rotation => rotation;
    private float rotation;

    private readonly float speed;

    private GameData gameData;
    public ShipModel(float speed, GameData gameData)
    {
        this.speed = speed;
        this.currentPosition = Vector2.zero;
        this.previousPosition = Vector2.zero;
        this.rotation = 0f;

        this.gameData = gameData;
    }
    public void Move()
    {
        Vector2 direction = new Vector2(Mathf.Cos((rotation+90f) * Mathf.Deg2Rad), Mathf.Sin((rotation+90f) * Mathf.Deg2Rad));
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
