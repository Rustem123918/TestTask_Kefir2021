using Supyrb;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private EAsteroidType asteroidType;

    [SerializeField]
    private float speed;

    [SerializeField]
    private GameObject asteroidMedium;
    [SerializeField]
    private GameObject asteroidSmall;

    [SerializeField]
    private GameObject deathVfx;

    private AddCoinsComponent addCoinsComponent;

    private AsteroidModel model;
    private void Awake()
    {
        addCoinsComponent = GetComponent<AddCoinsComponent>();
        var rot = transform.rotation.eulerAngles.z;
        model = new AsteroidModel(transform.position, rot, speed);
    }
    private void FixedUpdate()
    {
        model.Move();
        transform.position = model.CurrentPosition;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            CheckAsteroidType();
            addCoinsComponent.AddCoins();
            Destroy(gameObject);
            SpawnDeathEffect();
        }
        else if(collision.CompareTag("Laser"))
        {
            addCoinsComponent.AddCoins();
            Destroy(gameObject);
            SpawnDeathEffect();
        }
    }

    private void CheckAsteroidType()
    {
        switch (asteroidType)
        {
            case EAsteroidType.Large:
                Instantiate(asteroidMedium, transform.position, Quaternion.Euler(0f, 0f, GetRandomRotation()));
                Instantiate(asteroidMedium, transform.position, Quaternion.Euler(0f, 0f, GetRandomRotation()));
                break;
            case EAsteroidType.Medium:
                Instantiate(asteroidSmall, transform.position, Quaternion.Euler(0f, 0f, GetRandomRotation()));
                Instantiate(asteroidSmall, transform.position, Quaternion.Euler(0f, 0f, GetRandomRotation()));
                break;
            case EAsteroidType.Small:
                break;
        }
    }

    private float GetRandomRotation()
    {
        var r = Random.Range(2, 4);
        if (r % 2 == 0)
        {
            return Random.Range(1f, 179f);
        }
        else
        {
            return Random.Range(-1f, -179f);
        }
    }
    private void SpawnDeathEffect()
    {
        var effect = Instantiate(deathVfx, transform.position, Quaternion.identity);
        Destroy(effect, 2f);
    }
}
