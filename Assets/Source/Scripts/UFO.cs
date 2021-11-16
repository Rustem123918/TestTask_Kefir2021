using UnityEngine;

public class UFO : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private GameObject deathVfx;

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
        if (ship == null)
            return;
        model.UpdateShipPos(ship.transform.position);
        model.Move();
        transform.position = model.CurrentPosition;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet")  || collision.CompareTag("Laser"))
        {
            addCoinsComponent.AddCoins();
            Destroy(gameObject);
            var effect = Instantiate(deathVfx, transform.position, Quaternion.identity);
            Destroy(effect, 2f);
        }
    }
}
