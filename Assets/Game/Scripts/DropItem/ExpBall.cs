using UnityEngine;
using DG.Tweening;

public class ExpBall : MonoBehaviour
{
    [SerializeField]
    CircleCollider2D circleCollider;

    private void Awake() {
        circleCollider = GetComponent<CircleCollider2D>();
    }

    public void Init()
    {
        circleCollider.enabled = true;
    }

    public void Collect(Transform collideTrans)
    {
        circleCollider.enabled = false;
        transform.DOMove(collideTrans.transform.position,0.1f).OnComplete(()=>
        {
            gameObject.SetActive(false);
        });
    }
}