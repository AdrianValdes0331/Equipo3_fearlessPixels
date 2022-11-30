using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System;

public class BangLvl : MonoBehaviour
{
    public static event Action endFury;

    // Start is called before the first frame update
    private int bangLvl;
    private float totalDmgDiff;
    [HideInInspector] public bool isAvailable;
    private IEnumerator coroutine;
    public int cd;
    public Sprite normal;
    public Sprite Bang1;
    public Sprite Bang2;
    public Sprite Bang3;
    int livesStart;

    private void OnEnable()
    {
        PlayerController.startBang += countLives;
        Miauterito.bangEnd += countLivesAfter;
        Cowhuasplode.bangEnd += countLivesAfter;
        Misil.bangEnd += countLivesAfter;
    }
    private void OnDisable()
    {
        PlayerController.startBang -= countLives;
        Miauterito.bangEnd -= countLivesAfter;
        Cowhuasplode.bangEnd -= countLivesAfter;
        Misil.bangEnd -= countLivesAfter;
    }

    void Start()
    {

        bangLvl = 0;
        totalDmgDiff = 0;
        isAvailable = true;
        coroutine = WaitAndPrint(0.3f);
        StartCoroutine(coroutine);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void updateBangImage(Sprite img)
    {

        //Debug.Log(GameObject.Find("Canvas").GetComponent<PlayerDmg>().playerProfile[gameObject.name]);
        GameObject.Find("Canvas").GetComponent<PlayerDmg>().playerProfile[gameObject.name].transform.Find("bangLvl").GetComponent<Image>().sprite = img;
        //nImg = img;

    }

    public void bangUpdate(float dmg, bool done)
    {
        float currDmg = totalDmgDiff;
        if (!isAvailable) { return; }

        if (done)
        {
            totalDmgDiff += dmg;
        }
        else
        {
            totalDmgDiff = ((totalDmgDiff - dmg/2) < 0) ? 0 : totalDmgDiff - dmg/2;
        }

        Debug.Log(totalDmgDiff);

        if (totalDmgDiff >= 70 && totalDmgDiff<115) {

            bangLvl = 1;
            updateBangImage(Bang1);
        
        }
        else if(totalDmgDiff >= 115 && totalDmgDiff < 200)
        {
            bangLvl = 2;
            updateBangImage(Bang2);
        }
        else if(totalDmgDiff >= 200)
        {
            bangLvl = 3;
            updateBangImage(Bang3);
        }
        else
        {
            bangLvl = 0;
            updateBangImage(normal);
        }

    }

    public bool tryBang()
    {

        if(bangLvl != 0 && isAvailable)
        {
            StartCoroutine(cooldown());
            updateBangImage(normal);
            return true;
        }
        return false;

    }

    public float bangModifier(float baseDmg) {

        Debug.Log(bangLvl);
        Debug.Log(baseDmg);
        Debug.Log(baseDmg+((bangLvl-1)*(baseDmg*0.25f)));
        return baseDmg+((bangLvl-1)*(baseDmg*0.25f));

    }

    public IEnumerator cooldown()
    {
        SpriteRenderer sr = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        sr.DOColor(new Color(0.2f, 1, 0.2f, 1), 0.2f);
        isAvailable = false;
        yield return new WaitForSeconds(cd);
        endFury?.Invoke();
        sr.DOColor(new Color(1, 1, 1, 1), 0.2f);
        bangLvl = 0;
        totalDmgDiff = 0;
        isAvailable = true;
        updateBangImage(normal);
    }

    private IEnumerator WaitAndPrint(float waitTime)
    {

        yield return new WaitForSeconds(waitTime);
        updateBangImage(normal);
    }

    public void countLives() 
    {

        livesStart = gameObject.GetComponent<Respawn>().lives;
        Debug.Log(gameObject.name+" has "+livesStart+" lives");

    }

    public void countLivesAfter(int t)
    {
        Debug.Log("Waiting!");
        StartCoroutine(fury(t));
    }

    IEnumerator fury(int t)
    {
        yield return new WaitForSeconds(t);
        Debug.Log("Waited!");
        int livesEnd = gameObject.GetComponent<Respawn>().lives;
        Debug.Log("(Player, lives before, lives after): ("+gameObject.name+", "+livesStart+", "+livesEnd+")");
        if(livesStart == livesEnd && isAvailable)
        {
            Debug.Log(gameObject.name + " activated fury mode!!");
            SpriteRenderer sr = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
            sr.DOColor(new Color(1, 0.2f, 0.2f, 1), 0.2f);
            gameObject.transform.GetChild(0).GetChild(0).GetComponent<NHurtbox>().fury = 1.5f;
        }
    }

}
