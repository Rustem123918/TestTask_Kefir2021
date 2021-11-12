using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ShipModel : MoveableObjectBaseModel
{
    public float Velocity => (currentPosition - previousPosition).magnitude / Time.fixedDeltaTime;

    private Vector2 previousPosition;
    private float rotationSpeed;

    //private Dictionary<Vector2, Vector2> direction_velocity_map;
    public ShipModel(Vector2 startPosition, float startRotation, float speed, float rotationSpeed)
        : base(startPosition, startRotation, speed)
    {
        this.previousPosition = startPosition;
        this.rotationSpeed = rotationSpeed;

        //direction_velocity_map = new Dictionary<Vector2, Vector2>();
    }
    //public void Update()
    //{
    //    foreach (var d in direction_velocity_map)
    //    {
    //        direction_velocity_map[d.Key] = d.Value - d.Value * speed / 10f;
    //    }
    //}
    //private void Func()
    //{
    //    Vector2 direction = new Vector2(Mathf.Cos((rotation + 90f) * Mathf.Deg2Rad), Mathf.Sin((rotation + 90f) * Mathf.Deg2Rad));
    //    if (!direction_velocity_map.ContainsKey(direction))
    //        direction_velocity_map.Add(direction, direction.normalized * speed);
    //    else
    //        direction_velocity_map[direction] += direction_velocity_map[direction] * speed;
    //}
    public override void Move()
    {
        Vector2 direction = new Vector2(Mathf.Cos((rotation + 90f) * Mathf.Deg2Rad), Mathf.Sin((rotation + 90f) * Mathf.Deg2Rad));
        previousPosition = currentPosition;
        currentPosition = previousPosition + direction * speed * Time.fixedDeltaTime;
        //Func();
        //Vector2 direction = Vector2.zero;
        //foreach (var d in direction_velocity_map)
        //    direction += d.Value;
        //previousPosition = currentPosition;
        //currentPosition = previousPosition + direction  * Time.fixedDeltaTime;
        CheckBorders();
    }
    public void Stop()
    {
        previousPosition = currentPosition;
    }
    public void Rotate(ERotateDirection rotateDirection)
    {
        if (rotateDirection == ERotateDirection.Right)
        {
            rotation -= rotationSpeed * Time.fixedDeltaTime;
        }
        else if (rotateDirection == ERotateDirection.Left)
            rotation += rotationSpeed * Time.fixedDeltaTime;
    }
}
