using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Hurtbox : MonoBehaviour
{

    private Collider2D hcollider;
    private float dmgPercent = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        hcollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool getHitBy(float damage)
    {

        dmgPercent+=damage;
        GameObject.Find("Canvas").GetComponent<PlayerDmg>().playerProfile[transform.parent.name].GetComponent<TextMeshProUGUI>().text = dmgPercent+"%";
        return true;

    }

}
