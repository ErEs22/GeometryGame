using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public EnemyManager enemyManager;
    [HideInInspector]
    public PlayerControl playerControl;

    private void Awake() {
        playerControl = GetComponent<PlayerControl>();
    }
}
