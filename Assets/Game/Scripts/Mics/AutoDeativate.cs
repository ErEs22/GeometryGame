using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDeativate : MonoBehaviour
{
    public int deativateTime = 5;

    private void OnEnable() {
        Invoke(nameof(DeativateCurrentObject),deativateTime);
    }

    private void DeativateCurrentObject(){
        gameObject.SetActive(false);
    }
}
