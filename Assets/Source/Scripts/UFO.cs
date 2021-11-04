using UnityEngine;

public class UFO : MonoBehaviour
{
    [SerializeField]
    private GameData gameData;
    [SerializeField]
    private UFOData ufoData;

    private UFOModel model;

    private Vector2 shipPos;
    private void Awake()
    {
        shipPos = FindObjectOfType<Ship>().transform.position;
        model = new UFOModel(transform.position, shipPos, ufoData.Speed, gameData);
    }
    private void FixedUpdate()
    {
        shipPos = FindObjectOfType<Ship>().transform.position;
        model.UpdateShipPos(shipPos);
        model.Move();
        transform.position = model.CurrentPosition;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
            Destroy(gameObject);
    }
}
