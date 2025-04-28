using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Reeds input and decides actions taken by the player
/// </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Character character;
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference jumpAction;
    [SerializeField] private InputActionReference shootAction;
    [SerializeField] private GameObject weapon;
    [SerializeField] private float speed;
    [SerializeField] private float acceleration;

    private void OnEnable()
    {
        if (moveAction == null)
        {
            return;
        }

        if (shootAction == null)
        {
            return;
        }

        if(jumpAction == null)
        {
            return;
        }

        moveAction.action.performed += GetOnMove;
        jumpAction.action.performed += Jump;
        shootAction.action.performed += Shoot;
    }


    private void GetOnMove(InputAction.CallbackContext obj)
    {
        var request = new ForceRequest();
        var horizontalInput = obj.ReadValue<Vector2>();
        request.direction = new Vector3(horizontalInput.x, 0, horizontalInput.y);
        request.speed = speed;
        request.acceleration = acceleration;
        character.RequestForce(request);
    }
    private void Jump(InputAction.CallbackContext obj)
    {
        var request = new ForceRequest();
        var verticalInput = obj.ReadValue<Vector2>();
        request.direction = new Vector3(0, verticalInput.y, 0);
        request.acceleration = acceleration;
        character.RequestForce(request);
    }

    private void Shoot(InputAction.CallbackContext obj)
    {
        weapon.GetComponent<Weapon>().FireInstance();
    }
}