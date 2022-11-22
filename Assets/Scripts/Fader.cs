using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using DG.Tweening;

public class Fader : MonoBehaviour
{
    //Declare Variables
    //Duration and Color
    public Image Fight;  
    float normalReadyDuration = 1f;
    Color fadedTextColor = new Color(1f, 1f, 1f, 0f);
    
    void Start()
    {
        //Assign Color and Run Routine
        Fight.color = fadedTextColor;
        StartCoroutine(FadeImage(Fight, normalReadyDuration));
    }

    IEnumerator FadeImage(Image textImage, float normalDuration)
    {
        //After the Call, reduce image scale and slowly bring it back to it's size
        Fight.transform.localScale = Fight.transform.localScale / 2;
        Fight.transform.DOScale(1, 2).SetEase(Ease.OutBack).SetDelay(0.2f).SetEase(Ease.OutQuad).OnComplete(Ready);

        //Loop a fading effect on the desired image
        while (this.gameObject){
            textImage.DOFade(1f, normalDuration).SetEase(Ease.InQuint).OnComplete(
            () => textImage.DOFade(0f, normalDuration).SetEase(Ease.InQuint)
                );
            yield return new WaitForSeconds(normalDuration + normalDuration);
        }
    }

    //Didn't really had a use for OnComplete, So I just added a Log call.
    void Ready()
    {
        Debug.Log("DTCompleted");
    }
}
