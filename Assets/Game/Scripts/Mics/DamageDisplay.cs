using System.Threading;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class DamageDisplay : MonoBehaviour
{
    TextMeshPro text;

    private void Awake() {
        text = GetComponent<TextMeshPro>();
    }

    private void OnEnable() {
        DelayDeativate();
    }

    private void DelayDeativate()
    {
        float timer = 0;
        DOTween.To(() => timer, x => timer = x,1,0.2f).OnComplete(()=>{
            gameObject.SetActive(false);
        });
    }

    public void InitDisplayData(int damage,bool isCritical)
    {
        text.text = damage.ToString();
        if(isCritical)
        {
            text.color = Color.yellow;
        }
        else
        {
            text.color = Color.white;
        }
    }
}