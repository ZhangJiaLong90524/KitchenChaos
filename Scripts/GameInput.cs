using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public enum Binding
    {
        MoveUp,
        MoveDown,
        MoveLeft,
        MoveRight,
        Interact,
        InteractAlternate
    }

    private const string PlayerPrefBindings = "InputBindings";

    public static GameInput Instance;

    private PlayerInputActions _playerInputActions;

    private void Awake()
    {
        Instance = this;

        _playerInputActions = new PlayerInputActions();

        if (PlayerPrefs.HasKey(PlayerPrefBindings))
        {
            _playerInputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PlayerPrefBindings));
        }

        _playerInputActions.Player.Enable();

        _playerInputActions.Player.Interact.performed += Interact_performed;
        _playerInputActions.Player.InteractAlternate.performed += InteractAlternate_performed;
        _playerInputActions.Player.Pause.performed += Pause_performed;
    }

    private void OnDestroy()
    {
        _playerInputActions.Player.Interact.performed -= Interact_performed;
        _playerInputActions.Player.InteractAlternate.performed -= InteractAlternate_performed;
        _playerInputActions.Player.Pause.performed -= Pause_performed;

        _playerInputActions.Dispose();
    }

    private void Interact_performed(InputAction.CallbackContext context)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAlternate_performed(InputAction.CallbackContext context)
    {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    private void Pause_performed(InputAction.CallbackContext context)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    public event EventHandler OnPauseAction;
    public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnInteractAction;
    public event EventHandler OnBindingRebind;

    public Vector2 GetMovementVector()
    {
        var inputVector = _playerInputActions.Player.Move.ReadValue<Vector2>();
        return inputVector;
    }

    public string GetBindingKeyName(Binding binding)
    {
        return binding switch
        {
            Binding.MoveUp=>_playerInputActions.Player.Move.bindings[1].ToDisplayString(),
            Binding.MoveDown=>_playerInputActions.Player.Move.bindings[2].ToDisplayString(),
            Binding.MoveLeft=>_playerInputActions.Player.Move.bindings[3].ToDisplayString(),
            Binding.MoveRight=>_playerInputActions.Player.Move.bindings[4].ToDisplayString(),
            Binding.Interact=>_playerInputActions.Player.Interact.bindings[0].ToDisplayString(),
            Binding.InteractAlternate=>_playerInputActions.Player.InteractAlternate.bindings[0]
                .ToDisplayString(),
            _=>throw new ArgumentOutOfRangeException(nameof(binding), binding, null)
        };
    }

    public void RebindBinding(Binding binding, Action onReboundKey)
    {
        _playerInputActions.Player.Disable();


        InputAction inputAction;
        int bindingIndex;
        switch (binding)
        {
            case Binding.MoveUp:
                inputAction = _playerInputActions.Player.Move;
                bindingIndex = 1;
                break;
            case Binding.MoveDown:
                inputAction = _playerInputActions.Player.Move;
                bindingIndex = 2;
                break;
            case Binding.MoveLeft:
                inputAction = _playerInputActions.Player.Move;
                bindingIndex = 3;
                break;
            case Binding.MoveRight:
                inputAction = _playerInputActions.Player.Move;
                bindingIndex = 4;
                break;
            case Binding.Interact:
                inputAction = _playerInputActions.Player.Interact;
                bindingIndex = 0;
                break;
            case Binding.InteractAlternate:
                inputAction = _playerInputActions.Player.InteractAlternate;
                bindingIndex = 0;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(binding));
        }


        inputAction.PerformInteractiveRebinding(bindingIndex).OnComplete(_=>
        {
            _playerInputActions.Player.Enable();


            onReboundKey();


            PlayerPrefs.SetString(PlayerPrefBindings, _playerInputActions.SaveBindingOverridesAsJson());
            PlayerPrefs.Save();


            OnBindingRebind?.Invoke(this, EventArgs.Empty);
        }).Start();
    }
}