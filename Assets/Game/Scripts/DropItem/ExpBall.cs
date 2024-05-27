using UnityEngine;
using DG.Tweening;

public class ExpBall : MonoBehaviour
{
    [SerializeField]
    CircleCollider2D circleCollider;

    private void Awake() {
        circleCollider = GetComponent<CircleCollider2D>();
    }

    private void OnEnable() {
        EventManager.instance.onLevelEnd += CollectBonusCoin;
    }

    public void Init()
    {
        circleCollider.enabled = true;
    }

    public void Collect(Transform collideTrans)
    {
        EventManager.instance.onLevelEnd -= CollectBonusCoin;
        circleCollider.enabled = false;
        transform.DOMove(collideTrans.transform.position,0.1f).OnComplete(()=>
        {
            gameObject.SetActive(false);
        });
    }

    private void CollectBonusCoin()
    {
        EventManager.instance.OnChangeBonusCoinCount(1);
        EventManager.instance.onLevelEnd -= CollectBonusCoin;
    }
}