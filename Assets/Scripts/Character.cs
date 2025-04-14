using UnityEngine;

/// <summary>
/// Moves the character, controls everything related to the world and position
/// </summary> 

[RequireComponent(typeof(Rigidbody))]

public class Character : MonoBehaviour
{
    private ForceRequest _instantForceRequest;
    private ForceRequest _continousForceRequest;
    private Rigidbody _rigidbody;

    public void RequestForce(ForceRequest forceRequest)
    {
        _instantForceRequest = forceRequest;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
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
