using Supyrb;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Ship : MonoBehaviour
{
    public ShipModel ShipModel => model;

    [SerializeField]
    private float speed;
    [SerializeField]
    private float rotationSpeed;

    private Pistol pistol;
    private Laser laser;

    private bool laserShooting;
    private bool moveForward;
    private ERotateDirection rotateDirection;

    private ShipModel model;

    #region Input handlers
    public void PistolShoot(InputAction.CallbackContext context)
    {
        if (context.performed)
            pistol.Fire();
    }
    public void LaserShoot(InputAction.CallbackContext context)
    {
        if (context.performed)
            laserShooting = true;
        else if (context.canceled)
        {
            laser.StopFire();
            laserShooting = false;
        }
    }
    public void Move(InputAction.CallbackContext context)
    {
        if (context.performed)
            moveForward = true;
        else if (context.canceled)
            moveForward = false;
    }
    public void Rotate(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() == 1f)
            rotateDirection = ERotateDirection.Right;
        else if (context.ReadValue<float>() == -1f)
            rotateDirection = ERotateDirection.Left;
        else
            rotateDirection = ERotateDirection.None;
    }
    #endregion
    private void Awake()
    {
        model = new ShipModel(transform.position, 0f, speed, rotationSpeed);
        rotateDirection = ERotateDirection.None;
        pistol = GetComponent<Pistol>();
        laser = GetComponent<Laser>();
    }
    private void Update()
    {
        if (laserShooting)
            laser.Fire();
    }
    private void FixedUpdate()
    {
        if (moveForward)
            model.Move();
        else
            model.Stop();
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
