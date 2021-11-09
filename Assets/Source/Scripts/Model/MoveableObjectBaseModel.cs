using UnityEngine;

public abstract class MoveableObjectBaseModel
{
    public Vector2 CurrentPosition => currentPosition;
    protected Vector2 currentPosition;
    public float Rotation => rotation;
    protected float rotation;
    protected readonly float speed;

    protected Vector2 gameZone;
    public MoveableObjectBaseModel(Vector2 startPosition, float startRotation, float speed)
    {
        this.currentPosition = startPosition;
        this.rotation = startRotation;
        this.speed = speed;
        this.gameZone = SpawnerBaseModel.GameZone;
    }
    public abstract void Move();
    protected void CheckBorders()
    {
        var pos = currentPosition;

        if (pos.x > gameZone.x / 2f)
            pos.x = -gameZone.x / 2f;
        else if (pos.x < -gameZone.x / 2f)
            pos.x = gameZone.x / 2f;

        if (pos.y > gameZone.y / 2f)
            pos.y = -gameZone.y / 2f;
        else if (pos.y < -gameZone.y / 2f)
            pos.y = gameZone.y / 2f;

        currentPosition = pos;
    }
}
