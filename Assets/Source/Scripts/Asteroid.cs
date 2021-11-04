using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private GameData gameData;
    [SerializeField]
    private AsteroidData asteroidData;

    private AsteroidModel model;
    private void Awake()
    {
        var rot = transform.rotation.eulerAngles.z;
        model = new AsteroidModel(transform.position, rot, asteroidData.Speed, gameData);
    }
    private void FixedUpdate()
    {
        model.Move();
        transform.position = model.CurrentPosition;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
            Destroy(gameObject);
    }
}
