using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FacesAnimation : MonoBehaviour
{

    public GameObject carita1;
    public GameObject carita2;
    public GameObject carita3;
    public GameObject carita4;
    public GameObject carita5;

    private Vector3 leftRotation = new Vector3(0, 0, 15.756f); //Valor final de rotación a la izquierda
    private Vector3 rightRotation = new Vector3(0, 0, -15.756f); //Valor final de rotación a la derecha


    // Start is called before the first frame update
    void Start()
    {
        //Se crea una secuencia para cada carita pero hacen basicamente lo mismo que es moverse de izquierda a derecha
        // o de derecha a izquierda.

        Sequence caritas1Seq = DOTween.Sequence();
        caritas1Seq.Append(carita1.transform.DORotate(leftRotation, 1f).SetEase(Ease.OutCubic).SetDelay(0.2f)) // Rota hacia la izquierda
            .Append(carita1.transform.DORotate(rightRotation, 1f).SetEase(Ease.OutCubic).SetDelay(0.2f)) // Rota hacia la derecha
            .SetLoops(-1); // Se loopea para que se muevan mientras el jugador siga en esta pantalla

        Sequence caritas2Seq = DOTween.Sequence();
        caritas2Seq.Append(carita2.transform.DORotate(rightRotation, 1f).SetEase(Ease.OutCubic).SetDelay(0.2f)) // Rota hacia la derecha
            .Append(carita2.transform.DORotate(leftRotation, 1f).SetEase(Ease.OutCubic).SetDelay(0.2f)) // Rota hacia la izquierda
            .SetLoops(-1);

        Sequence caritas3Seq = DOTween.Sequence();
        caritas3Seq.Append(carita3.transform.DORotate(leftRotation, 1f).SetEase(Ease.OutCubic).SetDelay(0.2f)) // Rota hacia la izquierda
            .Append(carita3.transform.DORotate(rightRotation, 1f).SetEase(Ease.OutCubic).SetDelay(0.2f)) // Rota hacia la derecha
            .SetLoops(-1);

        Sequence caritas4Seq = DOTween.Sequence();
        caritas4Seq.Append(carita4.transform.DORotate(rightRotation, 1f).SetEase(Ease.OutCubic).SetDelay(0.2f)) // Rota hacia la derecha
            .Append(carita4.transform.DORotate(leftRotation, 1f).SetEase(Ease.OutCubic).SetDelay(0.2f)) // Rota hacia la izquierda
            .SetLoops(-1);

        Sequence caritas5Seq = DOTween.Sequence();
        caritas5Seq.Append(carita5.transform.DORotate(leftRotation, 1f).SetEase(Ease.OutCubic).SetDelay(0.2f)) // Rota hacia la izquierda
            .Append(carita5.transform.DORotate(rightRotation, 1f).SetEase(Ease.OutCubic).SetDelay(0.2f)) // Rota hacia la derecha
            .SetLoops(-1);

        // Las caritas 1,3 y 5 estan sincronizadas, al igual que la 2 y la 4.
    }
  
}
