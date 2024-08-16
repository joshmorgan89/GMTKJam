using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public PodMovement playerMovement;

    private PlayerInputAction playerInputAction;
    
    private InputAction moveAction;
    
    private void Awake()
    {
        playerInputAction = new PlayerInputAction();

        moveAction = playerInputAction.Player.Move;
    }

    private void OnEnable()
    {
        moveAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
    }

    private void FixedUpdate()
    {
        Vector2 moveDir = moveAction.ReadValue<Vector2>();
        playerMovement.Movement(moveDir);
    }
}
