using UnityEngine;

public class AsteroidModel : MoveableObjectBaseModel
{
    public AsteroidModel(Vector2 startPosition, float startRotation, float speed)
        : base(startPosition, startRotation, speed)
    {
    }
    public override void Move()
    {
        Vector2 direction = new Vector2(Mathf.Cos(rotation * Mathf.Deg2Rad), Mathf.Sin(rotation * Mathf.Deg2Rad));
        currentPosition = currentPosition + direction * speed * Time.fixedDeltaTime;
        CheckBorders();
    }
}
