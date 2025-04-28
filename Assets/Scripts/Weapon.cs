using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject prefabBullet;
    [SerializeField] private Transform tip;

    [ContextMenu("Fire")]
    public void FireInstance()
    {
        var newBullet = Instantiate(prefabBullet, tip.transform.position, tip.transform.rotation);
        newBullet.GetComponent<Bullet>().Fire();
    }
}