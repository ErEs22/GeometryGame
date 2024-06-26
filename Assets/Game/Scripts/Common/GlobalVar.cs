using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class GlobalVar : MonoBehaviour
{
    public static GlobalVar Instance;
    public static Transform playerTrans;
    public static float mapWidth = 50;
    public static float mapHeight = 50;
    public static eGameStatus gameStatus = eGameStatus.MainMenu;
    public CharacterData_SO currentPlayerData;

    private void Awake() {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        playerTrans = GameObject.FindGameObjectWithTag(GameConstant.playerTag).transform;
    }
}
