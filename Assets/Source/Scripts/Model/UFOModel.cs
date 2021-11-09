using UnityEngine;

public class UFOModel : MoveableObjectBaseModel
{
    private Vector2 shipPosition;
    public UFOModel(Vector2 shipPosition, Vector2 startPosition, float startRotation, float speed) 
        : base(startPosition, startRotation, speed)
    {
        this.shipPosition = shipPosition;
    }
    public void UpdateShipPos(Vector2 shipNewPos)
    {
        shipPosition = shipNewPos;
    }
    public override void Move()
    {
        LookAtShip();
        Vector2 direction = new Vector2(Mathf.Cos((rotation+90f) * Mathf.Deg2Rad), Mathf.Sin((rotation+90f) * Mathf.Deg2Rad));
        currentPosition = currentPosition + direction * speed * Time.fixedDeltaTime;
        CheckBorders();
    }
    private void LookAtShip()
    {
        var lookDir = shipPosition - currentPosition;
        var angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rotation = angle;
    }
}
