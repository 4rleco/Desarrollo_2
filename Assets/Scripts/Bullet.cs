using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Bullet : MonoBehaviour
{
    [SerializeField] private float force = 100;
    private new Rigidbody rigidbody;
    private Enemy enemy;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
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