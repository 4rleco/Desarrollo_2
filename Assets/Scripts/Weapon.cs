using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject prefabBullet;
    [SerializeField] private Transform tip;
    [SerializeField] private Transform gunPivot;

    [ContextMenu("Fire")]

    private void Update()
    {
        gameObject.transform.position = gunPivot.position;
    }

    public void FireInstance()
    {
        var newBullet = Instantiate(prefabBullet, tip.transform.position, tip.transform.rotation);
        newBullet.GetComponent<Bullet>().Fire();
    }
}