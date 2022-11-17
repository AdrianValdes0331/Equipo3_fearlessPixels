using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using DG.Tweening;

public class Fader : MonoBehaviour
{
    public Image Fight;
    float fastReadyDuration = 1f;
    float normalReadyDuration = 1f;
    Color fadedTextColor = new Color(1f, 1f, 1f, 0f);
    // Start is called before the first frame update
    void Start()
    {
        Fight.color = fadedTextColor;
        StartCoroutine(FadeImage(Fight, normalReadyDuration, fastReadyDuration));
    }

    IEnumerator FadeImage(Image textImage, float normalDuration, float fastDuration)
    {
        while(this.gameObject){
            textImage.DOFade(1f, fastDuration).SetEase(Ease.InQuint).OnComplete(
            () => textImage.DOFade(0f, normalDuration).SetEase(Ease.InQuint)
                );
            yield return new WaitForSeconds(normalDuration + fastDuration);
        }
    }
}
