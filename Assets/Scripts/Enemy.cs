using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] bool move = false;
    [SerializeField] private AnimationCurve travelCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    [SerializeField] private float cooldown = 1;
    [SerializeField] private Transform capsule;
    [SerializeField] private Transform from;
    [SerializeField] private Transform to;

    private int points = 10;
    private Player player;

    private void OnEnable()
    {
        if (move)
        {
            StartCoroutine(Animate()); 
        }
    }

    private IEnumerator Animate()
    {
        var travelDuration = travelCurve.keys[^1].time;
        while (true)
        {
            var travelTime = 0.0f;
            while (travelTime < travelDuration)
            {
                travelTime += Time.deltaTime;
                var t = travelCurve.Evaluate(travelTime / travelDuration);
                capsule.position = Vector3.Lerp(from.position, to.position, t);
                yield return null;
            }
            capsule.position = to.position;
            (from, to) = (to, from);

            yield return new WaitForSeconds(cooldown);
        }
    }
}
