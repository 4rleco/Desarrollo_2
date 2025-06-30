using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Bullet : MonoBehaviour
{
    [SerializeField] private float force = 100;
    public float lifetime;
    private new Rigidbody rigidbody;
    public Vector3 target { get; set; }
    public bool hit {  get; set; }
    private Enemy enemy;
    private float creationTime;
    public float damage;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();

        creationTime = Time.time;
    }

    private void Update()
    {
        if (Time.time > lifetime + creationTime)
        {
            Destroy(gameObject);
        }
    }

    public void Fire()
    {
        rigidbody.AddForce(transform.forward * force, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out enemy))
        {
            enemy.health -= damage;
            Debug.Log("Enemy hitted");
        }

        Destroy(gameObject);
    }
}