using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Enemy : MonoBehaviour,ITakeDamage
{
    private float moveSpeed = 5f;
    private EnemyManager enemyManager;

    private void Update() {
        MoveToPlayer();
    }

    public void Init(EnemyManager enemyManager){
        this.enemyManager = enemyManager;
    }

    private void MoveToPlayer()
    {
        float disToPlayer = Vector3.Distance(GlobalVar.playerObj.transform.position,transform.position);
        if(disToPlayer < 0.1f) return;
        Vector3 dirToPlayer = GlobalVar.playerObj.transform.position - transform.position;
        float angle = Mathf.Atan2(dirToPlayer.y,dirToPlayer.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle,transform.forward);
        transform.Translate(transform.right * moveSpeed * Time.deltaTime,Space.World);
    }

    public void TakeDamage()
    {
        Die();
    }

    private void Die(){
        transform.DOScale(2f,0.05f).OnComplete(() =>{
            gameObject.SetActive(false);
            enemyManager.enemies.Remove(this);
        });
    }
}
