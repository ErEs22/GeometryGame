using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class test : MonoBehaviour
{

    // private void Awake() {
    //     Debug.Log("This is before deative");
    //     gameObject.SetActive(false);
    //     Debug.Log("This is after deactive");
    // }
    
    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log(gameObject.name + "IS TRIGGER");
    }
}
