using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BangLvl : MonoBehaviour
{
    // Start is called before the first frame update
    private int bangLvl = 0;
    private float totalDmgDiff = 0;
    private bool isAvailable = true ;
    public int cd;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void updateBangText(string str)
    {

        Debug.Log(GameObject.Find("Canvas").GetComponent<PlayerDmg>().playerProfile[gameObject.name]);
        GameObject.Find("Canvas").GetComponent<PlayerDmg>().playerProfile[gameObject.name].transform.Find("bangLvl").GetComponent<TextMeshProUGUI>().text = str;

    }

    public void bangUpdate(float dmg, bool done)
    {

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
            updateBangText("-");
        
        }
        else if(totalDmgDiff >= 115 && totalDmgDiff < 200)
        {
            bangLvl = 2;
            updateBangText("--");
        }
        else if(totalDmgDiff >= 200)
        {
            bangLvl = 3;
            updateBangText("---");
        }
        else
        {
            bangLvl = 0;
            updateBangText("");
        }

    }

    public bool tryBang()
    {

        if(bangLvl != 0)
        {
            StartCoroutine(cooldown());
            bangLvl = 0;
            totalDmgDiff = 0;
            updateBangText("!!!");
            return true;
        }
        return false;

    }

    public float bangModifier(float baseDmg) {

        return baseDmg+((bangLvl-1)*(baseDmg*0.25f));

    }

    public IEnumerator cooldown()
    {
        isAvailable = false;
        yield return new WaitForSeconds(cd);
        isAvailable = true;
        updateBangText("");
    }

}
