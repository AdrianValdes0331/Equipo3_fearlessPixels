using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BasuraFall : MonoBehaviour
{
    public Transform Postition1;
    public Transform Postition2;
    public Transform Postition3;
    public Transform Postition4;
    public Transform Postition5;
    public Transform basura;
    public int i = 0;

    //SpriteRenderer trashSprite;
    Color fadedTrashColor = new Color(1f, 1f, 1f, 0f);
    Color normalTrashColor = new Color(1f, 1f, 1f, 1f);
    private float normalTrashDuration = 0.25f;
    private float fastTrashDuration = 0.05f;

    // Update is called once per frame
    void Update()
    {
        if (Time.timeSinceLevelLoad > i && GameObject.FindWithTag("Basura") == null)
        {
            int rInt = Random.Range(0, 4);
            int spawnran = Random.Range(20, 35);

            if (rInt == 0)
            {
                Instantiate(basura, Postition1.position, Quaternion.identity);
            }
            if (rInt == 1)
            {
                Instantiate(basura, Postition2.position, Quaternion.identity);
            }
            if (rInt == 2)
            {
                Instantiate(basura, Postition3.position, Quaternion.identity);
            }
            if (rInt == 3)
            {
                Instantiate(basura, Postition4.position, Quaternion.identity);
            }
            if (rInt == 4)
            {
                Instantiate(basura, Postition5.position, Quaternion.identity);
            }

            SpriteRenderer trashSprite = GameObject.FindWithTag("Basura").GetComponent<SpriteRenderer>();
            StartCoroutine(BlinkAndDestroy(trashSprite, normalTrashDuration, fastTrashDuration, 4));
            i += spawnran;
        }
        
    }

    IEnumerator BlinkAndDestroy(SpriteRenderer trashImage, float normalDuration, float fastDuration, int numberOfTimes)
    {
        yield return new WaitForSeconds(5f);

        for (int i = 0; i < numberOfTimes; i++)
        {
            trashImage.DOFade(1f, fastDuration).SetEase(Ease.InQuint).OnComplete(
                    () => trashImage.DOFade(0f, normalDuration).SetEase(Ease.InQuint)
                );
            yield return new WaitForSeconds(normalDuration + fastDuration);
        }
        Destroy(GameObject.FindWithTag("Basura"));
    }
}
