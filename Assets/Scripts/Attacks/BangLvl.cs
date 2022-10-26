using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BangLvl : MonoBehaviour
{
    // Start is called before the first frame update
    private int bangLvl;
    private float totalDmgDiff;
    private bool isAvailable;
    public int cd;
    public Sprite normal;
    public Sprite Bang1;
    public Sprite Bang2;
    public Sprite Bang3;

    void Start()
    {
        bangLvl = 0;
        totalDmgDiff = 0;
        isAvailable = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void updateBangImage(Sprite img)
    {

        //Debug.Log(GameObject.Find("Canvas").GetComponent<PlayerDmg>().playerProfile[gameObject.name]);
        GameObject.Find("Canvas").GetComponent<PlayerDmg>().playerProfile[transform.GetChild(0).name].transform.Find("bangLvl").GetComponent<Image>().sprite = img;
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
        isAvailable = false;
        yield return new WaitForSeconds(cd);
        bangLvl = 0;
        totalDmgDiff = 0;
        isAvailable = true;
        updateBangImage(normal);
    }

}
