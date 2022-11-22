using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.
using DG.Tweening;

public class OnHoverButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler
{
    private Vector3 initialPos = new Vector3(1, 1, 1); // Escala normal
    private Vector3 finalPos = new Vector3(1.3f, 1.3f, 1); // Escala agrandado


    public void OnPointerEnter(PointerEventData eventData) // Cuando el cursor esta encima del botón
    {
        gameObject.transform.DOScale(finalPos, 1f).SetEase(Ease.OutExpo); // Se hace grande la escala del botón
    }

    public void OnPointerExit(PointerEventData eventData) // Cuando el cursor ya no esta encima del botón
    {
        gameObject.transform.DOScale(initialPos, 1f).SetEase(Ease.OutExpo); // Se hace pequeña(normal) la escala del botón
    }

    public void OnSelect(BaseEventData eventData) // Cuando el botón es seleccionado
    {
        gameObject.transform.DOScale(initialPos, 1f).SetEase(Ease.OutExpo); //Se regresa a su escala normal
    }
}
