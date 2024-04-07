using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [HideInInspector]
    public int flySpeed = 20;
    [HideInInspector]
    public float lifeTime = 1f;
    [HideInInspector]
    public float damage = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Hit(other.gameObject);
    }

    protected virtual void Hit(GameObject hitObject)
    {
        hitObject.GetComponent<ITakeDamage>().TakeDamage(damage);
    }

    public void SetDelayDeativate()
    {
        Invoke(nameof(Deativate), lifeTime);
    }

    private void Deativate()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        Fly();
    }

    private void Fly()
    {
        transform.Translate(transform.right * flySpeed * Time.deltaTime, Space.World);
    }
}
