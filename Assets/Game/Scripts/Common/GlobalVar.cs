using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVar : MonoBehaviour
{
    public static GameObject playerObj;

    private void Awake() {
        playerObj = GameObject.FindGameObjectWithTag(GameConstant.playerTag);
    }
}
