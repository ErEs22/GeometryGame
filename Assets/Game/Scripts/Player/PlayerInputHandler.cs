using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour, PlayerControlInput.ILocomotionActions
{
    [DisplayOnly] public Vector2 moveInput;

    private PlayerControlInput playerControlInput;

    private void OnEnable() {
        InitializePlayerInput();
    }

    void InitializePlayerInput(){
        if(playerControlInput == null){
            playerControlInput = new PlayerControlInput();
        }
        playerControlInput.Enable();
        //Set All Action Maps Callbacks
        playerControlInput.Locomotion.SetCallbacks(this);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if(context.performed){
            moveInput = context.ReadValue<Vector2>();
        }else if(context.canceled){
            moveInput = Vector2.zero;
        }
    }
}
