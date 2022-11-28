using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    private PlayerInputAction _playerInputAction;
    public event Action AimCanceled;
    public event Action AimStarted;
    public event Action AimPerformed;
    public event Action JumpPerformed;
    public event Action AssistanControllStarted;
    public event Action AssistanControllCanceled;
    public event Action FastAttackPerformed;
    public event Action<float> MouseWheelPerformed;

    private void Awake()
    {
        Instance = this;
        _playerInputAction = new PlayerInputAction();
        _playerInputAction.Enable();

        _playerInputAction.FPS.AssistanControll.canceled += OnAssistanControllCanceled;
        _playerInputAction.FPS.AssistanControll.started += OnAssistanControllStarted;
        _playerInputAction.FPS.FastAttack.performed += OnFastAttackPerformed;
        _playerInputAction.FPS.MouseWheel.performed += OnMouseWheelPerformed;
        _playerInputAction.FPS.Jump.performed += OnJumpPerformed;
        _playerInputAction.FPS.Aim.performed += OnAimPerformed;
        _playerInputAction.FPS.Aim.canceled += OnAimCanceled;
        _playerInputAction.FPS.Aim.started += OnAimStarted;
    }

    private void OnEnable()
    {
        if (Instance is not null)
        {
            _playerInputAction.FPS.MouseWheel.performed += OnMouseWheelPerformed;
            _playerInputAction.FPS.Aim.canceled += OnAimCanceled;
            _playerInputAction.FPS.Aim.started += OnAimStarted;
            _playerInputAction.FPS.Aim.performed += OnAimPerformed;
            _playerInputAction.FPS.Jump.performed += OnJumpPerformed;
            _playerInputAction.FPS.AssistanControll.started += OnAssistanControllStarted;
            _playerInputAction.FPS.AssistanControll.canceled += OnAssistanControllCanceled;
            _playerInputAction.FPS.FastAttack.performed += OnFastAttackPerformed;
        }
    }
    private void OnDisable()
    {
        _playerInputAction.FPS.MouseWheel.performed -= OnMouseWheelPerformed;
        _playerInputAction.FPS.Aim.canceled -= OnAimCanceled;
        _playerInputAction.FPS.Aim.started -= OnAimStarted;
        _playerInputAction.FPS.Aim.performed -= OnAimPerformed;
        _playerInputAction.FPS.Jump.performed -= OnJumpPerformed;
        _playerInputAction.FPS.AssistanControll.started -= OnAssistanControllStarted;
        _playerInputAction.FPS.AssistanControll.canceled -= OnAssistanControllCanceled;
        _playerInputAction.FPS.FastAttack.performed -= OnFastAttackPerformed;
    }
    public bool PlayerAimHolding()
    {
        return _playerInputAction.FPS.Aim.inProgress;
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

    private void OnJumpPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj) =>
        JumpPerformed?.Invoke();
    
    private void OnAimStarted(UnityEngine.InputSystem.InputAction.CallbackContext obj) =>
        AimStarted?.Invoke();
    
    private void OnAimCanceled(UnityEngine.InputSystem.InputAction.CallbackContext obj) =>
        AimCanceled?.Invoke();
    
    private void OnAssistanControllStarted(UnityEngine.InputSystem.InputAction.CallbackContext obj) =>
        AssistanControllStarted?.Invoke();
    
    private void OnAssistanControllCanceled(UnityEngine.InputSystem.InputAction.CallbackContext obj) =>
        AssistanControllCanceled?.Invoke();
    
    private void OnAimPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj) =>
        AimPerformed?.Invoke();
    
    private void OnFastAttackPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj) =>
        FastAttackPerformed?.Invoke();

    private void OnMouseWheelPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj) => 
        MouseWheelPerformed?.Invoke(obj.ReadValue<float>());
}
