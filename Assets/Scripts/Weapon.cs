using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject prefabBullet;
    [SerializeField] private Transform tip;
    [SerializeField] private Transform gunPivot;
    public Transform cameraTransform;

    [Header("Bullet")]
    [SerializeField] private float bulletDamage;
    [SerializeField] private float bulletLifeTime;

    [ContextMenu("Fire")]

    private void Update()
    {
        gameObject.transform.position = gunPivot.position;
    }

    public void FireInstance()
    {
        RaycastHit hit;
        var newBullet = Instantiate(prefabBullet, tip.transform.position, tip.transform.rotation);
        Bullet bulletController = newBullet.GetComponent<Bullet>();
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, Mathf.Infinity))
        {
            bulletController.target = hit.point;
            bulletController.hit = true;
        }
        else
        {
            bulletController.target = cameraTransform.position + cameraTransform.forward;
            bulletController.hit = true;
        }
        
        newBullet.GetComponent<Bullet>().damage = bulletDamage;
        newBullet.GetComponent<Bullet>().lifetime = bulletLifeTime;
        newBullet.GetComponent<Bullet>().Fire();
    }
}