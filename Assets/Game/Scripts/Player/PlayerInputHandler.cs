using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour, PlayerControlInput.ILocomotionActions,PlayerControlInput.IUIActions
{
    public Vector2 moveInput;

    private PlayerControlInput playerControlInput;

    private void OnEnable() {
        InitializePlayerInput();
        EventManager.instance.onEnableLocomotionInput += EnableLocomotionInput;
        EventManager.instance.onDisableLocomotionInput += DisableLocomotionInput;
        EventManager.instance.onDisableUIInput += DisableUIInput;
        EventManager.instance.onEnableUIInput += EnableUIInput;
    }

    private void OnDisable() {
        EventManager.instance.onEnableLocomotionInput -= EnableLocomotionInput;
        EventManager.instance.onDisableLocomotionInput -= DisableLocomotionInput;
        EventManager.instance.onDisableUIInput -= DisableUIInput;
        EventManager.instance.onEnableUIInput -= EnableUIInput;
    }

    public void DisableLocomotionInput()
    {
        playerControlInput.Locomotion.Disable();
    }

    public void EnableLocomotionInput()
    {
        playerControlInput.Locomotion.Enable();
    }

    public void DisableUIInput()
    {
        playerControlInput.UI.Disable();
    }

    public void EnableUIInput()
    {
        playerControlInput.UI.Enable();
    }

    void InitializePlayerInput(){
        if(playerControlInput == null){
            playerControlInput = new PlayerControlInput();
        }
        playerControlInput.Enable();
        //Set All Action Maps Callbacks
        playerControlInput.Locomotion.SetCallbacks(this);
        playerControlInput.UI.SetCallbacks(this);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if(context.performed){
            moveInput = context.ReadValue<Vector2>();
        }else if(context.canceled){
            moveInput = Vector2.zero;
        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            EventManager.instance.OnOpenUI(eUIID.PauseMenu);
            DisableUIInput();
            Time.timeScale = 0;
        }
    }
}
