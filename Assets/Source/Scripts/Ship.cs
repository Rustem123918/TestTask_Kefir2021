using UnityEngine;
using UnityEngine.SceneManagement;

public class Ship : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float fireDelay;
    [SerializeField]
    private Transform firePosition;
    [SerializeField]
    private GameObject bulletPrefab;

    private float lastFireTime;
    private Vector2 movement;
    private Vector2 mousePos;
    private Camera cam;
    private AsteroidsSpawner spawner;
    private void Awake()
    {
        cam = Camera.main;
        spawner = FindObjectOfType<AsteroidsSpawner>();
        lastFireTime = 0f;
    }
    private void Update()
    {
        movement.y = Input.GetAxis("Vertical");
        movement.x = Input.GetAxis("Horizontal");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.Space) && (Time.time-lastFireTime > fireDelay))
        {
            FireBullet();
            lastFireTime = Time.time;
        }

        CheckBorders();
    }
    private void FixedUpdate()
    {
        MoveForward();
        LookAtMouse();
    }
    private void LookAtMouse()
    {
        var shipPos = (Vector2)transform.position;
        var lookDir = mousePos - shipPos;
        var angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

        var rot = transform.rotation.eulerAngles;
        rot.z = angle;
        transform.rotation = Quaternion.Euler(rot);
    }
    private void MoveForward()
    {
        var pos = transform.position;
        var move = movement * speed * Time.fixedDeltaTime;

        var shipPos = (Vector2)transform.position;
        var lookDir = mousePos - shipPos;
        Vector2 direction = new Vector2(Mathf.Cos(transform.rotation.z ), Mathf.Sin(transform.rotation.z ));
        //Debug.Log(direction);
        //transform.position = pos + (Vector3)lookDir * speed * Time.fixedDeltaTime * Mathf.Clamp(movement.y, 0, 1);
        transform.Translate(Mathf.Clamp(movement.y, 0, 1)*Vector3.up * speed * Time.fixedDeltaTime, Space.Self);
        //transform.position = pos + (Vector3)move;
    }
    private void FireBullet()
    {
        Instantiate(bulletPrefab, firePosition.position, firePosition.rotation);
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
        if (collision.CompareTag("Asteroid"))
            SceneManager.LoadScene(0);
    }
}
