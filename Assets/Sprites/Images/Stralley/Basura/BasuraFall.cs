using System.Collections;
using System.Collections.Generic;
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
    SpriteRenderer trashSprite;
    Color fadedTrashColor = new Color(1f, 1f, 1f, 0f);
    Color normalTrashColor = new Color(1f, 1f, 1f, 1f);

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
            trashSprite = GameObject.FindWithTag("Basura").GetComponent<SpriteRenderer>();
            StartCoroutine(BlinkAndDestroy());
            i += spawnran;
        }
        
    }

    IEnumerator BlinkAndDestroy()
    {
        yield return new WaitForSeconds(5f);
        for (int i = 0; i < 4; i++)
        {
            trashSprite.color = normalTrashColor;
            yield return new WaitForSeconds(0.5f);
            trashSprite.color = fadedTrashColor;
            yield return new WaitForSeconds(0.25f);
        }
        Destroy(GameObject.FindWithTag("Basura"));
    }
}
