using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private void Start()
    {
        Destroy(gameObject, 2f);
    }
    private void FixedUpdate()
    {
        transform.Translate(Vector3.up * speed * Time.fixedDeltaTime, Space.Self);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Asteroid") || collision.CompareTag("UFO"))
        {
            GetComponent<Collider2D>().enabled = false;
            Destroy(gameObject);
        }
    }
}
