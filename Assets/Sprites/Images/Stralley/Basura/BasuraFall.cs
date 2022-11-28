using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Photon.Pun;
using UnityEngine;

public class BasuraFall : MonoBehaviourPunCallbacks
{
    public Transform Postition1;
    public Transform Postition2;
    public Transform Postition3;
    public Transform Postition4;
    public Transform Postition5;

    public GameObject basuraOnline;
    public GameObject basuraOffline;
    private GameObject basuraObj;

    private int i = 0;
    private int isOnline;
    private int rInt;
    private int spawnran;

    //SpriteRenderer trashSprite;
    private float normalTrashDuration = 0.25f;
    private float fastTrashDuration = 0.05f;


    // Start is called before the first frame update
    void Start()
    {
        isOnline = PlayerPrefs.GetInt("isOnline");
        if (isOnline.Equals(1))
        {
            basuraOnline.SetActive(true);
            basuraObj = basuraOnline;
        }
        else
        {
            basuraOffline.SetActive(true);
            basuraObj = basuraOffline;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isOnline.Equals(1) && PhotonNetwork.IsMasterClient && (Time.timeSinceLevelLoad > i))
        {
            //isSpawned = true;
            rInt = Random.Range(0, 4);
            spawnran = Random.Range(20, 35);
            photonView.RPC("SpawnBasura", RpcTarget.All, rInt, spawnran);
        }
        else if (isOnline.Equals(0) && (Time.timeSinceLevelLoad > i))
        {
            rInt = Random.Range(0, 4);
            spawnran = Random.Range(20, 35);
            SpawnBasura(rInt, spawnran);
        }

    }

    [PunRPC]
    void SpawnBasura(int rInt, int spawnran)
    {
        i += spawnran;
        //if (Time.timeSinceLevelLoad > i)
        //{
        basuraObj.transform.localScale = new Vector3(0.70832f, 0.70832f, 0.70832f);
        basuraObj.SetActive(true);
        SpriteRenderer trashSprite = GameObject.FindWithTag("Basura").GetComponent<SpriteRenderer>();
        trashSprite.DOFade(1f, 0.2f).SetEase(Ease.InQuint);

        if (rInt == 0)
        {
            basuraObj.transform.position = Postition1.position;
        }
        if (rInt == 1)
        {
            basuraObj.transform.position = Postition2.position;
        }
        if (rInt == 2)
        {
            basuraObj.transform.position = Postition3.position;
        }
        if (rInt == 3)
        {
            basuraObj.transform.position = Postition4.position;
        }
        if (rInt == 4)
        {
            basuraObj.transform.position = Postition5.position;
        }

        StartCoroutine(BlinkAndDestroy(trashSprite, normalTrashDuration, fastTrashDuration));

        //i += spawnran;
        //}
    }

    IEnumerator BlinkAndDestroy(SpriteRenderer trashImage, float normalDuration, float fastDuration)
    {
        yield return new WaitForSeconds(5f);

        // La animación se repite un total de 4 veces
        for (int i = 0; i < 4; i++)
        {
            // Se modifica el alpha del sprite para que vaya de 1 a 0, una vez que
            //termina la primera parte (alpha en 1) empieza la segunda parte (poner alpha en 0)
            trashImage.DOFade(1f, fastDuration).SetEase(Ease.InQuint).OnComplete(
                    () => trashImage.DOFade(0f, normalDuration).SetEase(Ease.InQuint)
                );
            yield return new WaitForSeconds(normalDuration + fastDuration);
        }
        Debug.Log("destruyemee");
        basuraObj.transform.localScale = new Vector3(0, 0, 0);
        basuraObj.SetActive(false);
    }



}


