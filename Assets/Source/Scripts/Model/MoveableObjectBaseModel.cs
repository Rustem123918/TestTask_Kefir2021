using UnityEngine;

public abstract class MoveableObjectBaseModel
{
    public Vector2 CurrentPosition => currentPosition;
    protected Vector2 currentPosition;
    public float Rotation => rotation;
    protected float rotation;
    protected readonly float speed;

    protected GameData gameData;
    public MoveableObjectBaseModel(Vector2 startPosition, float startRotation, float speed, GameData gameData)
    {
        this.currentPosition = startPosition;
        this.rotation = startRotation;
        this.speed = speed;
        this.gameData = gameData;
    }
    public abstract void Move();
    protected void CheckBorders()
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
