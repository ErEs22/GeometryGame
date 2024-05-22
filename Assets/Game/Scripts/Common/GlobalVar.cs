using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class GlobalVar : MonoBehaviour
{
    public static Transform playerTrans;
    public static float mapWidth = 50;
    public static float mapHeight = 50;
    public static GameStatus gameStatus = GameStatus.MainMenu;

    private void Awake() {
        playerTrans = GameObject.FindGameObjectWithTag(GameConstant.playerTag).transform;
    }
}
