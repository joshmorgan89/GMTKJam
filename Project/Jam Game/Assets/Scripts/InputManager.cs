using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public PodMovement playerMovement;

    private PlayerInputAction _playerInputAction;
    
    private InputAction _moveAction;
    
    private void Awake()
    {
        _playerInputAction = new PlayerInputAction();

        _moveAction = _playerInputAction.Player.Move;
    }

    private void OnEnable()
    {
        _moveAction.Enable();
        _playerInputAction.Player.Break.performed += OnBreakPerformed;
        _playerInputAction.Player.Break.canceled += OnBreakCanceled;
        _playerInputAction.Player.Break.Enable();
    }

    private void OnDisable()
    {
        _moveAction.Disable();
        _playerInputAction.Player.Break.performed -= OnBreakPerformed;
        _playerInputAction.Player.Break.canceled -= OnBreakCanceled;
        _playerInputAction.Player.Break.Disable();
    }

    private void FixedUpdate()
    {
        Vector2 moveDir = _moveAction.ReadValue<Vector2>();
        playerMovement.Movement(moveDir);
    }

    private void OnBreakPerformed(InputAction.CallbackContext context)
    {
        playerMovement.BreakStart();
    }
    private void OnBreakCanceled(InputAction.CallbackContext context)
    {
        playerMovement.BreakEnd();
    }
}
