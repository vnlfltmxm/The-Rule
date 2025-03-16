using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Script.Util;

public class PlayerInputHandler
{
    private PlayerInput _playerInput;
    //입력 값
    public Vector2 MoveInputValue { get; private set; }
    public Vector2 LookInputValue { get; private set; }
    public bool IsSprint { get; private set; }
    public bool IsCrouch { get; private set; }

    //움직임 설정
    public bool IsAnalogMovement { get; private set; }

    //마우스 커서 설정
    public bool IsCursorLocked { get; private set; } = true;
    public bool IsCursorInputForLook { get; private set; } = true;

    public Action<bool> OnCrouchEvent;
    
    public Action OnInteractEvent;
    public Action OnPrimaryEvent;
    public Action OnSecondaryEvent;
    public Action OnCheckRulesEvent;
    
    public void Init(PlayerInput playerInput)
    {
        //PlayerInput.actions.FindAction()
        _playerInput = playerInput;
        SetActive(true);
    }

    public void SetActive(bool isActive)
    {
        if (isActive == true)
        {
            _playerInput.actions[InputType.Move.ToString()].started   += OnMove;
            _playerInput.actions[InputType.Move.ToString()].performed += OnMove;
            _playerInput.actions[InputType.Move.ToString()].canceled  += OnMove;
        
            _playerInput.actions[InputType.Sprint.ToString()].started   += OnSprint;
            _playerInput.actions[InputType.Sprint.ToString()].performed += OnSprint;
            _playerInput.actions[InputType.Sprint.ToString()].canceled  += OnSprint;
        
            _playerInput.actions[InputType.Crouch.ToString()].started   += OnCrouch;
            _playerInput.actions[InputType.Crouch.ToString()].performed += OnCrouch;
            _playerInput.actions[InputType.Crouch.ToString()].canceled  += OnCrouch;
        
            _playerInput.actions[InputType.Interact.ToString()].started   += OnInteract;
            _playerInput.actions[InputType.Interact.ToString()].performed += OnInteract;
            _playerInput.actions[InputType.Interact.ToString()].canceled  += OnInteract;
        
            _playerInput.actions[InputType.PrimaryAction.ToString()].started   += OnPrimaryAction;
            _playerInput.actions[InputType.PrimaryAction.ToString()].performed += OnPrimaryAction;
            _playerInput.actions[InputType.PrimaryAction.ToString()].canceled  += OnPrimaryAction;
        
            _playerInput.actions[InputType.SecondaryAction.ToString()].started   += OnSecondaryAction;
            _playerInput.actions[InputType.SecondaryAction.ToString()].performed += OnSecondaryAction;
            _playerInput.actions[InputType.SecondaryAction.ToString()].canceled  += OnSecondaryAction;
        
            _playerInput.actions[InputType.CheckRules.ToString()].started   += OnCheckRules;
            _playerInput.actions[InputType.CheckRules.ToString()].performed += OnCheckRules;
            _playerInput.actions[InputType.CheckRules.ToString()].canceled  += OnCheckRules;
        
            _playerInput.actions[InputType.Look.ToString()].started   += OnLook;
            _playerInput.actions[InputType.Look.ToString()].performed += OnLook;
            _playerInput.actions[InputType.Look.ToString()].canceled  += OnLook;
        }
        else
        {
            _playerInput.actions[InputType.Move.ToString()].started   -= OnMove;
            _playerInput.actions[InputType.Move.ToString()].performed -= OnMove;
            _playerInput.actions[InputType.Move.ToString()].canceled  -= OnMove;
        
            _playerInput.actions[InputType.Sprint.ToString()].started   -= OnSprint;
            _playerInput.actions[InputType.Sprint.ToString()].performed -= OnSprint;
            _playerInput.actions[InputType.Sprint.ToString()].canceled  -= OnSprint;
        
            _playerInput.actions[InputType.Crouch.ToString()].started   -= OnCrouch;
            _playerInput.actions[InputType.Crouch.ToString()].performed -= OnCrouch;
            _playerInput.actions[InputType.Crouch.ToString()].canceled  -= OnCrouch;
        
            _playerInput.actions[InputType.Interact.ToString()].started   -= OnInteract;
            _playerInput.actions[InputType.Interact.ToString()].performed -= OnInteract;
            _playerInput.actions[InputType.Interact.ToString()].canceled  -= OnInteract;
        
            _playerInput.actions[InputType.PrimaryAction.ToString()].started   -= OnPrimaryAction;
            _playerInput.actions[InputType.PrimaryAction.ToString()].performed -= OnPrimaryAction;
            _playerInput.actions[InputType.PrimaryAction.ToString()].canceled  -= OnPrimaryAction;
        
            _playerInput.actions[InputType.SecondaryAction.ToString()].started   -= OnSecondaryAction;
            _playerInput.actions[InputType.SecondaryAction.ToString()].performed -= OnSecondaryAction;
            _playerInput.actions[InputType.SecondaryAction.ToString()].canceled  -= OnSecondaryAction;
        
            _playerInput.actions[InputType.CheckRules.ToString()].started   -= OnCheckRules;
            _playerInput.actions[InputType.CheckRules.ToString()].performed -= OnCheckRules;
            _playerInput.actions[InputType.CheckRules.ToString()].canceled  -= OnCheckRules;
        
            _playerInput.actions[InputType.Look.ToString()].started   -= OnLook;
            _playerInput.actions[InputType.Look.ToString()].performed -= OnLook;
            _playerInput.actions[InputType.Look.ToString()].canceled  -= OnLook;
        }
    }
    
    #region Input 콜백 함수
    private void OnMove(InputAction.CallbackContext value)
    {
        MoveInputValue = value.ReadValue<Vector2>().normalized;
    }
    private void OnSprint(InputAction.CallbackContext value)
    {
        IsSprint = value.ReadValueAsButton();
    }
    private void OnCrouch(InputAction.CallbackContext value)
    {
        IsCrouch = value.ReadValueAsButton();
        OnCrouchEvent?.Invoke(IsCrouch);
    }
    private void OnInteract(InputAction.CallbackContext value)
    {
        if (value.started)
            OnInteractEvent?.Invoke();
    }
    private void OnPrimaryAction(InputAction.CallbackContext value)
    {
        if (value.started)
            OnPrimaryEvent?.Invoke();
    }
    private void OnSecondaryAction(InputAction.CallbackContext value)
    {
        if (value.started)
            OnSecondaryEvent?.Invoke();
    }
    private void OnCheckRules(InputAction.CallbackContext value)
    {
        if (value.started)
            OnCheckRulesEvent?.Invoke();
    }
    private void OnLook(InputAction.CallbackContext value)
    {
        LookInputValue = value.ReadValue<Vector2>();
    }
    #endregion
}
