using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    [SerializeField]
    private float followReactTime = 0.1f;

    private void Update() {
        FollowingPlayer();
    }

    private void FollowingPlayer(){
        Vector3 targetPos = Vector3.Lerp(transform.position,GlobalVar.playerObj.position,followReactTime);
        targetPos.z = -10;
        transform.position = targetPos;
    }
}
