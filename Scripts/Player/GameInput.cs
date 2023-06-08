using System;
using UnityEngine;


public class GameInput : MonoBehaviour
{
    public static GameInput instance { get; private set; }
    private PlayerInputActions playerInputActions;
    public event EventHandler OnInteractActions;
    public event EventHandler OnInteractAltnateActions;
    public event EventHandler OnPauseAction;

    public enum Binding { 
        Move_up, Move_down,Move_Left,Move_Right,Interact,Interact_alt, Pause,
    
    }
    private void Awake()
    {
        instance = this;


        playerInputActions = new PlayerInputActions();
        playerInputActions.playerAction.Enable();
        playerInputActions.playerAction.Interaction.performed += Interaction_performed;
        playerInputActions.playerAction.InteracAltnate.performed += InteracAltnate_performed;
        playerInputActions.playerAction.Pause.performed += Pause_performed;
    }

    private void OnDestroy()
    {
        playerInputActions.playerAction.Interaction.performed -= Interaction_performed;
        playerInputActions.playerAction.InteracAltnate.performed -= InteracAltnate_performed;
        playerInputActions.playerAction.Pause.performed -= Pause_performed;

        playerInputActions.Dispose();
    }

    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    private void InteracAltnate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if(OnInteractActions!= null)
        {
            OnInteractAltnateActions(obj, EventArgs.Empty);
        }
        
    } private void Interaction_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if(OnInteractActions!= null)
        {
            OnInteractActions(obj, EventArgs.Empty);
        }
        
    }

    public Vector2 GetInputNomalized()
    {
        Vector2 InputVector = playerInputActions.playerAction.Movement.ReadValue<Vector2>();       
        InputVector = InputVector.normalized;
        return InputVector;
    }

    public string GetBindingText(Binding binding)
    {
        switch (binding)
        {
            default:
            case Binding.Interact:
                {
                    return playerInputActions.playerAction.Interaction.bindings[0].ToDisplayString();
                }
            case Binding.Interact_alt:
                {
                    return playerInputActions.playerAction.InteracAltnate.bindings[0].ToDisplayString();
                }
            case Binding.Pause:
                {
                    return playerInputActions.playerAction.Pause.bindings[0].ToDisplayString();
                }
            case Binding.Move_up:
                {
                    return playerInputActions.playerAction.Movement.bindings[1].ToDisplayString();
                }
            case Binding.Move_down:
                {
                    return playerInputActions.playerAction.Movement.bindings[2].ToDisplayString();
                }
            case Binding.Move_Right:
                {
                    return playerInputActions.playerAction.Movement.bindings[4].ToDisplayString();
                }
            case Binding.Move_Left:
                {
                    return playerInputActions.playerAction.Movement.bindings[3].ToDisplayString();
                }
        }
    }

}
