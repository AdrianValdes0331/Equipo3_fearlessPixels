using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectorAnim : MonoBehaviour
{
    Color startColor;
    Vector3 startScale;
    Vector3 targetScale;
    // Start is called before the first frame update
    void Start()
    {
        startColor = gameObject.GetComponent<Image>().color;
        startScale = transform.localScale;
        targetScale = new Vector3(startScale.x + 0.1f, startScale.y + 0.1f, startScale.z + 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        Debug.Log("mouse entered");
        Sequence anim = DOTween.Sequence();
        anim.Append(transform.DOShakePosition(0.2f, 5, 20));
        anim.Append(transform.DOScale(targetScale, 0.3f).SetEase(Ease.OutBack));
        anim.Insert(0.2f, gameObject.GetComponent<Image>().DOColor(new Color(0.5f, 0.5f, 0.55f, 1), 0.3f).SetEase(Ease.OutBack));
    }

    private void OnMouseExit()
    {
        Debug.Log("mouse exit");
        exit();
    }

    public void selected() 
    {

        //Text textbox = (Text) gameObject.transform.GetChild(0).GetComponent<TextMeshPro>();
        //string str = textbox.text;
        transform.DOShakePosition(0.2f, 5, 20).SetDelay(0.1f).OnComplete(exit);

    }

    private void exit()
    {
        Sequence anim = DOTween.Sequence();
        anim.Append(transform.DOScale(startScale, 0.3f));
        anim.Insert(0, gameObject.GetComponent<Image>().DOColor(startColor, 0.3f));
    }
}
