using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Reeds input and decides actions taken by the player
/// </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Player player;
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


    private void GetOnMove(InputAction.CallbackContext ctx)
    {
        var request = new ForceRequest();
        var horizontalInput = ctx.ReadValue<Vector2>();
        request.direction = new Vector3(horizontalInput.x, 0, horizontalInput.y);
        request.speed = speed;
        request.acceleration = acceleration;
        player.RequestForce(request);
    }
    private void Jump(InputAction.CallbackContext ctx)
    {
        var request = new ForceRequest();
        request.direction = Vector3.up;
        request.speed = speed;
        request.acceleration = acceleration;
        player.RequestForce(request);
    }

    private void Shoot(InputAction.CallbackContext ctx)
    {
        weapon.GetComponent<Weapon>().FireInstance();
    }
}