using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(PlayerInputHandler))]
[RequireComponent(typeof(PlayerManager))]
public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10;
    [SerializeField] private float turnSpeed = 0.1f;
    private PlayerInputHandler playerInputHandler;
    private PlayerManager playerManager;

    private void OnEnable() {
        playerInputHandler = GetComponent<PlayerInputHandler>();
        playerManager = GetComponent<PlayerManager>();
    }

    private void Update() {
        HandleMove();
        HandleRotation();
    }

    void HandleMove(){
        transform.position += (Vector3)playerInputHandler.moveInput * moveSpeed * Time.deltaTime;
    }

    void HandleRotation(){
        if(playerInputHandler.moveInput == Vector2.zero) return;
        float angle = Mathf.Atan2(playerInputHandler.moveInput.y,playerInputHandler.moveInput.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.AngleAxis(angle,Vector3.forward),turnSpeed);
    }
}
