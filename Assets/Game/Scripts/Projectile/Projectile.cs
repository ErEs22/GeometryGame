using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private int flySpeed = 20;

    private void Update() {
        Fly();
    }

    private void Fly(){
        transform.Translate(transform.right * flySpeed * Time.deltaTime,Space.World);
    }
}
