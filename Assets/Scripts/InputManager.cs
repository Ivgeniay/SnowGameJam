using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    private PlayerInputAction _playerInputAction;
    public event Action OnAimCanceled;

    private void Awake()
    {
        Instance = this;
        _playerInputAction = new PlayerInputAction();

        _playerInputAction.FPS.Aim.canceled += Aim_canceled;
    }

    private void Start()
    {
        EnableFPSControls();
    }
    
    private void EnableFPSControls()
    {
        _playerInputAction.FPS.Enable();
    }

    public Vector2 GetPlayerMovement()
    {
        return _playerInputAction.FPS.Movement.ReadValue<Vector2>();
    }
    
    public Vector2 GetPlayerLook()
    {
        return _playerInputAction.FPS.Look.ReadValue<Vector2>();
    }
    
    public bool PlayerJumpedThisFrame()
    {
        return _playerInputAction.FPS.Jump.triggered;
    }

    public bool IsPlayerAiming()
    {
        return _playerInputAction.FPS.Aim.IsPressed();
    }

    private void Aim_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnAimCanceled?.Invoke();
    }
}
