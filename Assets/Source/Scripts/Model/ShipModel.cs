using UnityEngine;

public class ShipModel : MoveableObjectBaseModel
{
    public float Velocity => (currentPosition - previousPosition).magnitude / Time.fixedDeltaTime;

    private Vector2 previousPosition;

    public ShipModel(Vector2 startPosition, float startRotation, float speed, GameData gameData)
        : base(startPosition, startRotation, speed, gameData)
    {
        this.previousPosition = startPosition;
    }
    public override void Move()
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
}
