using Supyrb;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ship : MonoBehaviour
{
    [SerializeField]
    private PistolData pistolData;
    [SerializeField]
    private float speed;

    [SerializeField]
    private Transform firePosition;
    [SerializeField]
    private GameObject bulletPrefab;

    private float movementY;
    private Vector2 mousePos;
    private Camera cam;

    private ShipModel model;
    private PistolModel pistolModel;
    private void Awake()
    {
        model = new ShipModel(transform.position, 0f, speed);
        pistolModel = new PistolModel(pistolData.FireDelay);
        pistolModel.OnFire += FireBullet;
        cam = Camera.main;
    }
    private void Update()
    {
        movementY = Input.GetAxis("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            pistolModel.Fire();
        }
    }
    private void FixedUpdate()
    {
        MoveForward();
        transform.position = model.CurrentPosition;
        LookAtMouse();
        transform.rotation = Quaternion.Euler(0f, 0f, model.Rotation);
    }
    private void LookAtMouse()
    {
        var shipPos = (Vector2)transform.position;
        var lookDir = mousePos - shipPos;
        var angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

        model.Rotate(angle);

        //var rot = transform.rotation.eulerAngles;
        //rot.z = angle;
        //transform.rotation = Quaternion.Euler(rot);
    }
    private void MoveForward()
    {
        if (movementY != 0f)
            model.Move();
    }
    private void FireBullet()
    {
        Instantiate(bulletPrefab, firePosition.position, firePosition.rotation);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Asteroid") || collision.CompareTag("UFO"))
        {
            Signals.Clear();
            SceneManager.LoadScene(0);
        }
    }
}
