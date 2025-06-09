using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Reeds input and decides actions taken by the player
/// </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Player player;
    [Header("Inputs")]
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference jumpAction;
    [SerializeField] private InputActionReference shootAction;
    [SerializeField] private InputActionReference lookAction;
    [Header("Objects")]
    [SerializeField] private GameObject weapon;
    [SerializeField] private Transform cameraTransform;
    [Header("Camera Options")]
    [SerializeField] private float sensitivity = 2.0f;
    [SerializeField] private float verticalClamp = 90.0f;
    [SerializeField] private Vector2 rotation;
    [Header("Player speed")]
    [SerializeField] private float speed;
    [SerializeField] private float acceleration;

    private float verticalRotation = 0.0f;
    private Vector2 lookInput;

    private bool grounded;
    private int amountOfJumps;

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

        if (jumpAction == null)
        {
            return;
        }

        if (lookAction == null)
        {
            return;
        }

        moveAction.action.performed += GetOnMove;
        jumpAction.action.performed += Jump;
        shootAction.action.performed += Shoot;
        lookAction.action.performed += Look;

        weapon.transform.SetParent(cameraTransform);
    }

    private void Look(InputAction.CallbackContext ctx)
    {
        lookInput = lookAction.action.ReadValue<Vector2>() * sensitivity;

        //horizontal rotation
        transform.Rotate(Vector3.up * lookInput.x);

        //vertical rotation
        verticalRotation -= lookInput.y;
        verticalRotation = Mathf.Clamp(verticalRotation, -verticalClamp, verticalClamp); //prevents the camera to exceed maximum vertical angles
        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0.0f, 0.0f);
    }

    private void GetOnMove(InputAction.CallbackContext ctx)
    {
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0.0f;
        right.y = 0.0f;

        forward.Normalize();
        right.Normalize();        

        var request = new ForceRequest();
        var horizontalInput = ctx.ReadValue<Vector2>();
        request.direction = right * horizontalInput.x + forward * horizontalInput.y;
        request.speed = speed;
        request.acceleration = acceleration;
        player.RequestForce(request);            
    }   

    private void Jump(InputAction.CallbackContext ctx)
    {
        if (grounded && amountOfJumps > 0)
        {
            var request = new ForceRequest();
            request.direction = Vector3.up;
            request.speed = speed;
            request.acceleration = acceleration;
            player.RequestForce(request);

            grounded = false;
            amountOfJumps = 0;
        }
    }

    private void Shoot(InputAction.CallbackContext ctx)
    {
        weapon.GetComponent<Weapon>().FireInstance();
    }

    private void OnCollisionEnter(Collision collision)
    {
        grounded = true;
        amountOfJumps += 1;
    }
}