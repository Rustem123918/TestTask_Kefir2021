using UnityEngine;

public class UFO : MonoBehaviour
{
    [SerializeField]
    private GameData gameData;
    [SerializeField]
    private float speed;

    private UFOModel model;

    private Ship ship;
    private void Awake()
    {
        ship = FindObjectOfType<Ship>();
        transform.rotation = Quaternion.identity;
        model = new UFOModel(ship.transform.position, transform.position, 0f, speed, gameData);
    }
    private void FixedUpdate()
    {
        model.UpdateShipPos(ship.transform.position);
        model.Move();
        transform.position = model.CurrentPosition;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
            Destroy(gameObject);
    }
}
