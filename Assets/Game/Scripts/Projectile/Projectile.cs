using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private int flySpeed = 20;
    public float lifeTime = 1f;

    public void SetDelayDeativate(){
        Invoke(nameof(Deativate),lifeTime);
    }

    private void Deativate(){
        gameObject.SetActive(false);
    }

    private void Update() {
        Fly();
    }

    private void Fly(){
        transform.Translate(transform.right * flySpeed * Time.deltaTime,Space.World);
    }
}
