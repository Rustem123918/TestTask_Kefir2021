using UnityEngine;

public class UFO : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private Ship ship;
    private AddCoinsComponent addCoinsComponent;

    private UFOModel model;
    private void Awake()
    {
        ship = FindObjectOfType<Ship>();
        addCoinsComponent = GetComponent<AddCoinsComponent>();
        transform.rotation = Quaternion.identity;
        model = new UFOModel(ship.transform.position, transform.position, 0f, speed);
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
        {
            addCoinsComponent.AddCoins();
            Destroy(gameObject);
        }

    }
}
