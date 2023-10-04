using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
    public PlayerInput playerInput;

    private Vector2 movementInput;
    private Vector2 aimInput;
    private bool interactPressed = false;
    private bool submitPressed = false;
    private bool pausePressed = false;
    private bool firePressed = false;
    private bool reloadPressed = false;

    private static InputManager _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Debug.LogError("There are more than one instance of InputManager in the scene");
        }

        _instance = this;
    }

    public static InputManager GetInstance()
    {
        return _instance;
    }

    public void MovePressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            movementInput = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            movementInput = context.ReadValue<Vector2>();
        }
    }
    public void AimPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            movementInput = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            movementInput = context.ReadValue<Vector2>();
        }
    }

    public Vector2 GetMoveDirection()
    {
        return movementInput;
    }

    public Vector3 GetAimDirection()
    {
        return aimInput;
    }

    public void InteractPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            interactPressed = true;
        }
        else if (context.canceled)
        {
            interactPressed = false;
        }
    }

    public void FirePressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            firePressed = true;
        }
        else if (context.canceled)
        {
            firePressed = false;
        }
    }

    public void ReloadPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            reloadPressed = true;
        }
        else if (context.canceled)
        {
            reloadPressed = false;
        }
    }

    //Bool commented out to get desired effect of being able to hold to shoot
    public bool GetFirePressed()
    {
        bool result = firePressed;
        //firePressed = false;
        return result;
    }

    public bool GetReloadPressed()
    {
        bool result = reloadPressed;
        reloadPressed = false;
        return result;
    }

    public bool GetInteractPressed()
    {
        bool result = reloadPressed;
        interactPressed = false;
        return result;
    }

}