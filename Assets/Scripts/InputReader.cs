using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class InputReader : MonoBehaviour
{
    [SerializeField] InputActionReference moveAction;
    [SerializeField] InputActionReference jumpAction;
    [SerializeField] InputActionReference shootAction;
    [SerializeField] private float speed = 10;
    [SerializeField] private float jumpForce = 10;
    private Vector2 inputValue;
    private Vector3 playerMovement;
    private bool isJumpRequested;

    public static event Action fire;

    private void OnEnable()
    {
        moveAction.action.started += HandleMoveInput;
        moveAction.action.performed += HandleMoveInput;
        moveAction.action.canceled += HandleMoveInput;
        jumpAction.action.started += HandleJumpInput;
        shootAction.action.started += HandleTriggerInput;
    }

    private void HandleMoveInput(InputAction.CallbackContext ctx)
    {
        inputValue = ctx.ReadValue<Vector2>();
        playerMovement = new Vector3(inputValue.x, 0, inputValue.y);
        Debug.Log(inputValue);
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
        
        if(isJumpRequested)
        {
            isJumpRequested = false;
            gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce * Time.fixedDeltaTime, ForceMode.Force);
        }
    }
}
