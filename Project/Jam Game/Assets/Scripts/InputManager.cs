using System;
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
        _playerInputAction.Player.Zoom1.performed += OnZoom1Performed;
        _playerInputAction.Player.Zoom2.performed += OnZoom2Performed;
        _playerInputAction.Player.Break.Enable();
        _playerInputAction.Player.Zoom1.Enable();
        _playerInputAction.Player.Zoom2.Enable();

    }

    private void OnDisable()
    {
        _moveAction.Disable();
        _playerInputAction.Player.Break.performed -= OnBreakPerformed;
        _playerInputAction.Player.Break.canceled -= OnBreakCanceled;
        _playerInputAction.Player.Zoom1.performed -= OnZoom1Performed;
        _playerInputAction.Player.Zoom2.performed -= OnZoom2Performed;
        _playerInputAction.Player.Break.Disable();
        _playerInputAction.Player.Zoom1.Disable();
        _playerInputAction.Player.Zoom2.Disable();
    }

    private void FixedUpdate()
    {
        Vector2 moveDir = _moveAction.ReadValue<Vector2>();
        playerMovement.Movement(moveDir);
    }

    private void OnZoom2Performed(InputAction.CallbackContext context)
    {
        Debug.Log(2);
        GameManager.Instance.SetZoomLevelTwo(); 
    }

    private void OnZoom1Performed(InputAction.CallbackContext context)
    {

        Debug.Log(1);
        GameManager.Instance.SetZoomLevelOne();
    }
    private void OnBreakPerformed(InputAction.CallbackContext context)
    {
        Debug.Log(3);
        playerMovement.BreakStart();
    }
    private void OnBreakCanceled(InputAction.CallbackContext context)
    {
        playerMovement.BreakEnd();
    }
}
