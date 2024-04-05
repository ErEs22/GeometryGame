using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVar : MonoBehaviour
{
    public static Transform playerObj;

    private void Awake() {
        playerObj = GameObject.FindGameObjectWithTag(GameConstant.playerTag).transform;
    }
}
