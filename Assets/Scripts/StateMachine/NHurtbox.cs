using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class NHurtbox : MonoBehaviour
{
    [HideInInspector] public float fury = 1.0f;
    public GameObject ItsAHit;
    private Collider2D hcollider;
    public float tankiness = 2.0f;
    [HideInInspector] public float dmgPercent = 0.0f;
    [HideInInspector] public event Action<Vector2> HitReact;

    void OnEnable()
    {
        BangLvl.endFury += returnToNormal;
    }

    void OnDisable()
    {
        BangLvl.endFury -= returnToNormal;
    }

    public bool getHitBy(float damage, int force, int angle, float xPos)
    {

        BangLvl bang = transform.parent.transform.parent.GetComponent<BangLvl>();
        bang.bangUpdate(damage, false);
        //alreveza el angulo dependiendo si el ataque esta a la derecha o izquierda
        if (transform.position.x - xPos < 0) { angle = 180-angle; }
        float radian = angle * Mathf.Deg2Rad;
        Vector2 finalForce = new Vector2(Mathf.Cos(radian), Mathf.Sin(radian)) * force;
        print("Collider: " + gameObject.name);
        print("Fuerza base = " + force);
        print("angulo = " + angle);
        print("Fuerza final = "+finalForce);
        if(!bang.isAvailable)
        {
            dmgPercent += damage*fury;
        }
        else
        {
            dmgPercent += damage;
        }
        //transform.parent.GetComponent<Rigidbody2D>().AddForce(finalForce*((dmgPercent/100)/ tankiness));
        HitReact?.Invoke(finalForce*((dmgPercent/100)/ tankiness));
        //GameObject EXSound = Instantiate(ItsAHit, transform.position, transform.rotation, transform.parent);
        //Destroy(EXSound, 2.0f);
        Debug.Log(transform.parent);
        UpdateDmgPercentText();
        return true;

    }

    public void UpdateDmgPercentText()
    {
        GameObject.Find("Canvas").GetComponent<PlayerDmg>().playerProfile[transform.parent.transform.parent.name].transform.Find("dmgPercent").GetComponent<TextMeshProUGUI>().text = System.Math.Round(dmgPercent, 2) + "%";
    }

    public void returnToNormal()
    {
        fury = 1.0f;
    }

}
