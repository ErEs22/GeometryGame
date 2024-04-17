using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    public LayerMask colliderLayers;
    public int damageAmount;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer == colliderLayers){
            other.GetComponent<ITakeDamage>().TakeDamage(damageAmount);
        }
    }
}
