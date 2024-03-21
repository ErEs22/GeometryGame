using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDeativate : MonoBehaviour
{
    public float deativateTime = 5;

    private void OnEnable() {
        print("CCCCCCCCCCCCCCC" + deativateTime);
    }

    public void DelayDeativate(){
        Invoke(nameof(DeativateCurrentObject),deativateTime);
    }

    private void DeativateCurrentObject(){
        gameObject.SetActive(false);
    }
}
