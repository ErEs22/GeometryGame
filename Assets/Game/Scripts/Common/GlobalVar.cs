using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVar : MonoBehaviour
{
    public static Transform playerTrans;

    private void Awake() {
        playerTrans = GameObject.FindGameObjectWithTag(GameConstant.playerTag).transform;
    }
}
