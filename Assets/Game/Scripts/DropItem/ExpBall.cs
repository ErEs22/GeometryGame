using UnityEngine;
using DG.Tweening;

public class ExpBall : MonoBehaviour
{
    [SerializeField]
    CircleCollider2D circleCollider;
    [HideInInspector]
    public bool isBonus = false;

    private void Awake() {
        circleCollider = GetComponent<CircleCollider2D>();
    }

    private void OnEnable() {
        EventManager.instance.onLevelEnd += CollectBonusCoin;
    }

    public void Init(bool isBonus = false)
    {
        this.isBonus = isBonus;
        circleCollider.enabled = true;
        if(isBonus)
        {
            EventManager.instance.OnChangeBonusCoinCount(-1);
            transform.localScale = new Vector3(0.5f,0.5f,0.5f);
        }
        else
        {
            transform.localScale = new Vector3(0.3f,0.3f,0.3f);
        }
    }

    private void ClearBall()
    {
        circleCollider.enabled = false;
        gameObject.SetActive(false);
    }

    public void Collect(Transform collideTrans)
    {
        EventManager.instance.onLevelEnd -= CollectBonusCoin;
        circleCollider.enabled = false;
        transform.DOMove(collideTrans.transform.position,0.1f).OnComplete(()=>
        {
            gameObject.SetActive(false);
            AudioManager.Instance.PlaySFX(AudioManager.Instance.expCollectSFX);
        });
    }

    private void CollectBonusCoin()
    {
        EventManager.instance.OnChangeBonusCoinCount(1);
        EventManager.instance.onLevelEnd -= CollectBonusCoin;
        ClearBall();
    }
}