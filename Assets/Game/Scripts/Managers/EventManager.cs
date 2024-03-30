using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;
    public event UnityAction<Enemy> enemyDie;

    private void Awake() {
        if(instance == null)
        {
            instance = this;
        }
    }

    
}
