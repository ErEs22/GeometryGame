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
    public int damage = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Hit(other);
    }

    protected virtual void Hit(Collider2D otherCollider)
    {
        otherCollider.TryGetComponent<ITakeDamage>(out ITakeDamage damageObject);
        if(damageObject != null)
        {
            damageObject.TakeDamage(damage);
        }
        else
        {
            Debug.Log("碰到的物体没有继承ITakeDamage接口，若需要处理，请继承该接口");
        }
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

    protected virtual void Fly()
    {
        transform.Translate(transform.right * flySpeed * Time.deltaTime, Space.World);
    }
}
