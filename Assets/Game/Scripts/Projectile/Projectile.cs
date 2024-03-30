using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    public int flySpeed = 20;
    public float lifeTime = 1f;


    private void OnTriggerEnter2D(Collider2D other)
    {
        Hit(other.gameObject);
    }

    protected virtual void Hit(GameObject hitObject)
    {
        //根据碰到的物体的Tag来确定受击影响
        switch (hitObject.tag)
        {
            case "Player":
                break;
            case "Enemy":
                hitObject.GetComponent<ITakeDamage>().TakeDamage();
                break;
            default: break;
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

    private void Fly()
    {
        transform.Translate(transform.right * flySpeed * Time.deltaTime, Space.World);
    }
}
