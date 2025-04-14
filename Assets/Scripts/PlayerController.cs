using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Reeds input and decides actions taken by the player
/// </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Character character;
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private float speed;
    [SerializeField] private float acceleration;

    private void OnEnable()
    {
        if (moveAction == null)
        {
            return;
        }

        moveAction.action.performed += GetOnMove;
    }

    private void  GetOnMove(InputAction.CallbackContext obj)
    {
        var request = new ForceRequest();
        var horizontalInput = obj.ReadValue<Vector2>();
        request.direction = new Vector3(horizontalInput.x, 0, horizontalInput.y);
        request.speed = speed;
        request.acceleration = acceleration;
        character.RequestForce(request);
    }
}