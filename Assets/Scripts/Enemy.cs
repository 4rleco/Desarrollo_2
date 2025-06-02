using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] bool move = false;
    [SerializeField] bool morePositions = false;
    [SerializeField] private AnimationCurve travelCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    [SerializeField] private float cooldown = 1;
    [SerializeField] private Transform capsule;
    [SerializeField] private Transform right;
    [SerializeField] private Transform left;
    [SerializeField] private Transform front;
    [SerializeField] private Transform back;

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
                capsule.position = Vector3.Lerp(right.position, left.position, t);
                yield return null;
            }
            if (morePositions)
            {
                capsule.position = left.position;
                (left, front) = (front, right);
                (right, back) = (back, left);

            }
            else
            {
                capsule.position = left.position;
                (left, right) = (right, left);
            }

            yield return new WaitForSeconds(cooldown);
        }
    }
}
