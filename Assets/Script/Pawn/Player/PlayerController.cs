using UnityEngine;
using UnityEngine.InputSystem;
using Script.Util;

public class PlayerController
{
    public void Init(PlayerInput playerInput)
    {
        //PlayerInput.actions.FindAction()
        playerInput.actions[InputType.Move.ToString()].started   += OnMove;
        playerInput.actions[InputType.Move.ToString()].performed += OnMove;
        playerInput.actions[InputType.Move.ToString()].canceled  += OnMove;
        
        playerInput.actions[InputType.Sprint.ToString()].started   += OnSprint;
        playerInput.actions[InputType.Sprint.ToString()].performed += OnSprint;
        playerInput.actions[InputType.Sprint.ToString()].canceled  += OnSprint;
        
        playerInput.actions[InputType.Crouch.ToString()].started   += OnCrouch;
        playerInput.actions[InputType.Crouch.ToString()].performed += OnCrouch;
        playerInput.actions[InputType.Crouch.ToString()].canceled  += OnCrouch;
        
        playerInput.actions[InputType.Interact.ToString()].started   += OnInteract;
        playerInput.actions[InputType.Interact.ToString()].performed += OnInteract;
        playerInput.actions[InputType.Interact.ToString()].canceled  += OnInteract;
        
        playerInput.actions[InputType.PrimaryAction.ToString()].started   += OnPrimaryAction;
        playerInput.actions[InputType.PrimaryAction.ToString()].performed += OnPrimaryAction;
        playerInput.actions[InputType.PrimaryAction.ToString()].canceled  += OnPrimaryAction;
        
        playerInput.actions[InputType.SecondaryAction.ToString()].started   += OnSecondaryAction;
        playerInput.actions[InputType.SecondaryAction.ToString()].performed += OnSecondaryAction;
        playerInput.actions[InputType.SecondaryAction.ToString()].canceled  += OnSecondaryAction;
        
        playerInput.actions[InputType.CheckRules.ToString()].started   += OnCheckRules;
        playerInput.actions[InputType.CheckRules.ToString()].performed += OnCheckRules;
        playerInput.actions[InputType.CheckRules.ToString()].canceled  += OnCheckRules;
        
        playerInput.actions[InputType.Look.ToString()].started   += OnLook;
        playerInput.actions[InputType.Look.ToString()].performed += OnLook;
        playerInput.actions[InputType.Look.ToString()].canceled  += OnLook;
    }
    
    #region Input 콜백 함수
    private void OnMove(InputAction.CallbackContext value)
    {
        if (value.started)
        {
        }
        else if (value.performed)
        {
        }
        else if (value.canceled)
        {
        }
    }
    private void OnSprint(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            
        }
    }
    private void OnCrouch(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            
        }
    }
    private void OnInteract(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            
        }
    }
    private void OnPrimaryAction(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            
        }
    }
    private void OnSecondaryAction(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            
        }
    }
    private void OnCheckRules(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            
        }
    }
    private void OnLook(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            
        }
    }
    #endregion
}
