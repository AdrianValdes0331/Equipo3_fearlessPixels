using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using DG.Tweening;
using TMPro;

public class AnimateTrainingHUD : MonoBehaviour
{

    List<GameObject> HUDElements = new List<GameObject>();
    List<GameObject> PlayerImages = new List<GameObject>();
    List<GameObject> LifeImages = new List<GameObject>();
    Color opaqueLifeColor = new Color(0.5f, 0.3f, 0.3f);
    Color brightLifeColor = new Color(1f, 1f, 1f);
    float introDelay = 0.5f;
    float scaleHUDDelay = 0.15f;
    float changeColorHUDDelay = 1f;
    float fadeInHUDDuration = 5f;
    float scaleHUDDuration = 1.75f;
    float changeColorHUDDuration = 0.25f;
    int lifeColorChangeTimes = 11;
    int livesNumber;

    void Start()
    {
        StartCoroutine(StartingHUDAnimation());
    }

    //Corrutina para animar el HUD al comienzo de la partida
    IEnumerator StartingHUDAnimation()
    {
        //Se hace una pequeña pausa para darle tiempo a la instanciación de elementos de la UI
        yield return new WaitForSeconds(introDelay);
        HUDElements.AddRange(GameObject.FindGameObjectsWithTag("HUD"));
        //Todos los elementos encontrados con la etiqueta "HUD" y sus hijos son mandados a hacer FadeIn
        foreach (GameObject hudElement in HUDElements)
        {
            Image elementImage = hudElement.GetComponent<Image>();
            if (elementImage)
            {
                //Se manda al padre a hacer FadeIn
                FadeInHUDImage(elementImage);
            }

            //Todos los hijos son mandados a hacer FadeIn
            FadeInChilds(hudElement.transform);
        }

        PlayerImages.AddRange(GameObject.FindGameObjectsWithTag("PlayerImage"));
        //Todos los elementos encontrados con la etiqueta "PlayerImage" (ilustración de peleadores) son mandados a escalarse
        foreach (GameObject playerImage in PlayerImages)
        {
            ScaleHUDTransform(playerImage.transform, 1.25f, 1f);
        }

        LifeImages.AddRange(GameObject.FindGameObjectsWithTag("LifeImage"));
        //Todos los elementos encontrados con la etiqueta "LifeImage" (corazones de vidas) son mandados a escalarse y a cambiar de color
        foreach (GameObject lifeImage in LifeImages)
        {
            Image image = lifeImage.GetComponent<Image>();
            StartCoroutine(ChangeColorHUDImage(image));
            ScaleHUDTransform(lifeImage.transform, 0.2f, 0.08f);
        }
    }

    //Aplica el FadeIn a todos los hijos del Transform mandado y hace recursión para hacerlo extensivamente
    public void FadeInChilds(Transform element)
    {
        foreach (Transform child in element)
        {
            Image childImage = child.gameObject.GetComponent<Image>();
            if (childImage)
            {
                //Si se reconoce un componente Image en el elemento, se manda a llamar FadeIn con su respectiva función
                FadeInHUDImage(childImage);
            }
            else
            {
                TextMeshProUGUI childText = child.gameObject.GetComponent<TextMeshProUGUI>();
                if (childText)
                {
                    //Si se reconoce un componente TextMeshProUGUI en el elemento, se manda a llamar FadeIn con su respectiva función
                    FadeInHUDText(childText);
                }
            }
            //Aquí se aplica la recursión mandando a llamar la función nuevamente con cada hijo
            FadeInChilds(child);
        }
    }

    //Aplica un FadeIn de Image con DOTween - DOFade
    public void FadeInHUDImage(Image image)
    {
        /*Previamente, todos los elementos del HUD en escena son puestos con su alpha en 0f
        para lograr conseguir el efecto esperado. La transicion se hace con una curva OutQuart,
        comenzando con una velocidad media y finalizando con una lenta (tangente horizontalmente)*/
        image.DOFade(1f, fadeInHUDDuration).SetEase(Ease.OutQuart);
    }

    //Aplica un FadeIn de TextMeshPro con DOTween - DOFade
    public void FadeInHUDText(TextMeshProUGUI text)
    {
        //Las propiedades son iguales a las de FadeInHUDImage
        text.DOFade(1f, fadeInHUDDuration).SetEase(Ease.OutQuart);
    }

    //Aplica un Scale de Transform con DOTween - DOScale
    public void ScaleHUDTransform(Transform transform, float bigScale, float finalScale)
    {
        /*Previamente, los elementos a los que se aplica la transición son escalados con un tamaño menor
        al estándar final. La animación comienza escalando el transform a un tamaño un poco mayor al deseado (bigScale),
        con una curva InQuad (cada vez más rápida) y un pequeño delay. Luego, al completarse la transición pasada, 
        el elemento se escala a su tamaño final correcto (finalScale), con una curva OutExpo, comenzando con una 
        velocidad rápida y finalizando con una lenta (tangente horizontalmente)*/
        transform.DOScale(bigScale, scaleHUDDuration).SetEase(Ease.InQuad).SetDelay(scaleHUDDelay).OnComplete(
                    () => transform.DOScale(finalScale, scaleHUDDuration / 2).SetEase(Ease.OutExpo)
                );
    }

    //Corrutina que aplica un Color Change de Image con DOTween - DOColor cierto número de veces
    IEnumerator ChangeColorHUDImage(Image image)
    {
        //Se hace una pequeña pausa para aplicar el delay de animación deseado
        yield return new WaitForSeconds(changeColorHUDDelay);
        //La animación se realiza cierto número de veces por medio de un ciclo for
        for (int i = 0; i < lifeColorChangeTimes; i++)
        {
            /*Si el index del for es par, se hace la transición a un color brillante de la vida.
            si es impar, se hace la transición al color opaco de la vida*/
            if (i % 2 == 0)
            {
                /*La transición se realiza con una curva InOutCubic y una duración corta*/
                image.DOColor(brightLifeColor, changeColorHUDDuration).SetEase(Ease.InOutCubic);
            }
            else
            {
                /*La transición se realiza con una curva OutQuart y una duración corta*/
                image.DOColor(opaqueLifeColor, changeColorHUDDuration).SetEase(Ease.OutQuart);
            }
            //Se hace una pausa después de cada vuelta en el for para darle tiempo a la transición en ejecución de terminar antes de la siguiente
            yield return new WaitForSeconds(changeColorHUDDuration + 0.1f);
        }
    }

    //Corrutina que aplica un FadeIn y FadeOut intermitente de Image con DOTween - DOFade
    IEnumerator BlinkImage(Image textImage, float normalDuration, float fastDuration, int numberOfTimes)
    {
        //La animación se realiza cierto número de veces por medio de un ciclo for
        for (int i = 0; i < numberOfTimes; i++)
        {
            /*Previamente, la imagen es puesta con su alpha en 0f para lograr conseguir el efecto esperado.
            La transicion comienza haciendo FadeIn con una duracion rápida y una curva InQuint. Al finalizar,
            la animación continúa haciendo FadeOut con una duración corta y la misma curva InQuint*/
            textImage.DOFade(1f, fastDuration).SetEase(Ease.InQuint).OnComplete(
                    () => textImage.DOFade(0f, normalDuration).SetEase(Ease.InQuint)
                );
            //Se hace una pausa después de cada vuelta en el for para darle tiempo a las 2 transiciones antes de repetirlas
            yield return new WaitForSeconds(normalDuration + fastDuration);
        }
    }
}
