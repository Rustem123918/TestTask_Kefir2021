using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private AsteroidsSpawner spawner;
    private void Awake()
    {
        spawner = FindObjectOfType<AsteroidsSpawner>();
    }
    private void Start()
    {
        var rot = new Vector3(0f, 0f, Random.Range(0f, 359f));
        transform.rotation = Quaternion.Euler(rot);
    }
    private void Update()
    {
        CheckBorders();
    }
    private void FixedUpdate()
    {
        transform.Translate(Vector3.up * (speed * Time.fixedDeltaTime), Space.Self);
    }
    private void CheckBorders()
    {
        var pos = transform.position;

        if (pos.x > spawner.SpawnZone.x / 2f)
            pos.x = -spawner.SpawnZone.x / 2f;
        else if (pos.x < -spawner.SpawnZone.x / 2f)
            pos.x = spawner.SpawnZone.x / 2f;

        if (pos.y > spawner.SpawnZone.y / 2f)
            pos.y = -spawner.SpawnZone.y / 2f;
        else if (pos.y < -spawner.SpawnZone.y / 2f)
            pos.y = spawner.SpawnZone.y / 2f;

        transform.position = pos;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
            Destroy(gameObject);
    }
}
