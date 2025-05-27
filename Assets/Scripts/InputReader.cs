using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

public class InputReader : MonoBehaviour
{
    [SerializeField] InputActionReference moveAction;
    [SerializeField] InputActionReference jumpAction;
    [SerializeField] InputActionReference shootAction;
    [SerializeField] InputActionReference lookAction;
    [SerializeField] private float speed = 10;
    [SerializeField] private float jumpForce = 10;
    private Vector2 inputValue;
    private Vector2 rotateValue;
    private Vector3 playerMovement;
    private Vector3 cameraMovement;
    private bool isJumpRequested;

    public static event Action fire;

    int amountOfJumps = 1;
    bool grounded = true;

    private void OnEnable()
    {
        moveAction.action.started += HandleMoveInput;
        moveAction.action.performed += HandleMoveInput;
        moveAction.action.canceled += HandleMoveInput;
        jumpAction.action.started += HandleJumpInput;
        shootAction.action.started += HandleTriggerInput;
        lookAction.action.started += HandleLookInput;
    }

    private void HandleLookInput(InputAction.CallbackContext ctx)
    {
        rotateValue = ctx.ReadValue<Vector2>();
        cameraMovement = new Vector3(rotateValue.x, rotateValue.y, 0);
    }

    private void HandleMoveInput(InputAction.CallbackContext ctx)
    {
        inputValue = ctx.ReadValue<Vector2>();
        playerMovement = new Vector3(inputValue.x, 0, inputValue.y);
    }   

    private void HandleTriggerInput(InputAction.CallbackContext ctx)
    {
        fire?.Invoke();
    }

    private void HandleJumpInput(InputAction.CallbackContext ctx)
    {
        isJumpRequested = true;
    }

    private void FixedUpdate()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(playerMovement * speed * Time.fixedDeltaTime, ForceMode.Acceleration);

        gameObject.transform.position = cameraMovement * Time.fixedDeltaTime;  

        if (isJumpRequested)
        {
            isJumpRequested = false;
            gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce * Time.fixedDeltaTime, ForceMode.Force);
        }
    }
}
