using Supyrb;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ship : MonoBehaviour
{
    public LaserModel LaserModel => laser.LaserModel;

    [SerializeField]
    private float speed;
    [SerializeField]
    private float rotationSpeed;

    private Pistol pistol;
    private Laser laser;

    private bool moveForward;
    private ERotateDirection rotateDirection;

    private ShipModel model;
    private void Awake()
    {
        model = new ShipModel(transform.position, 0f, speed, rotationSpeed);
        rotateDirection = ERotateDirection.None;
        pistol = GetComponent<Pistol>();
        laser = GetComponent<Laser>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
            rotateDirection = ERotateDirection.Right;
        else if (Input.GetKeyUp(KeyCode.D))
            rotateDirection = ERotateDirection.None;
        else if (Input.GetKeyDown(KeyCode.A))
            rotateDirection = ERotateDirection.Left;
        else if (Input.GetKeyUp(KeyCode.A))
            rotateDirection = ERotateDirection.None;

        if (Input.GetKeyDown(KeyCode.W))
            moveForward = true;
        else if (Input.GetKeyUp(KeyCode.W))
            moveForward = false;

        if (Input.GetKeyDown(KeyCode.K))
            pistol.Fire();
        else if (Input.GetKey(KeyCode.L))
            laser.Fire();
        else if (Input.GetKeyUp(KeyCode.L))
            laser.StopFire();

        //model.Update();
    }
    private void FixedUpdate()
    {
        if (moveForward)
            model.Move();
        transform.position = model.CurrentPosition;

        model.Rotate(rotateDirection);
        transform.rotation = Quaternion.Euler(0f, 0f, model.Rotation);
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
