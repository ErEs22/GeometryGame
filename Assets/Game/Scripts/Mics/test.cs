using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class test : MonoBehaviour
{

    private void Awake() {
        Debug.Log("This is before deative");
        gameObject.SetActive(false);
        Debug.Log("This is after deactive");
    }

    private void OnEnable() {
        transform.DOScale(5.0f,1.0f).SetRelative().OnStepComplete(()=>{
            transform.DOScale(-5.0f,1.0f).SetRelative();
        });
    }
}
