using UnityEngine;

/// <summary>
/// Moves the player, controls everything related to the world and position
/// </summary> 

[RequireComponent(typeof(Rigidbody))]

public class Player : MonoBehaviour
{
    private ForceRequest _instantForceRequest;
    private ForceRequest _continousForceRequest;
    private Rigidbody _rigidbody;

    public static Transform instance;

    public void RequestForce(ForceRequest forceRequest)
    {
        _instantForceRequest = forceRequest;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();

        if (instance != null && instance != transform)
        {
            Debug.LogWarning($"{name}: <color=red>An instance was already created!<color=red>");
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (_continousForceRequest != null)
        {
            var sppedPercentage = _rigidbody.linearVelocity.magnitude / _continousForceRequest.speed;
            var remainingSpeedPercentage = Mathf.Clamp01(1.0f - sppedPercentage);
            _rigidbody.AddForce(_continousForceRequest.direction * _continousForceRequest.acceleration * remainingSpeedPercentage, ForceMode.Force);

        }
        if (_instantForceRequest == null)
        {
            return;
        }

        _rigidbody.AddForce(_instantForceRequest.direction * _instantForceRequest.acceleration, ForceMode.Impulse);
        _instantForceRequest = null;
    }
}
