using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Bullet : MonoBehaviour
{
    [SerializeField] private float force = 100;
    [SerializeField]private float lifetime = 2.0f;
    private new Rigidbody rigidbody;
    private Enemy enemy;
    private float creationTime;

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
        Destroy(gameObject);
    }
}