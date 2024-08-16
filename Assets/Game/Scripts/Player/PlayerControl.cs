using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(PlayerInputHandler))]
[RequireComponent(typeof(PlayerManager))]
public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10;
    [SerializeField] private float turnSpeed = 0.05f;
    private PlayerInputHandler playerInputHandler;
    private PlayerManager playerManager;
    private Rigidbody2D rig;

    private void Awake() {
        rig = GetComponent<Rigidbody2D>();
    }

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
        // rig.velocity = playerInputHandler.moveInput * moveSpeed;//TODO 解决使用刚体速度移动产生抖动的问题
    }

    void HandleRotation(){
        if(playerInputHandler.moveInput == Vector2.zero) return;
        float angle = Mathf.Atan2(playerInputHandler.moveInput.y,playerInputHandler.moveInput.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.AngleAxis(angle,Vector3.forward),turnSpeed);
    }
}
