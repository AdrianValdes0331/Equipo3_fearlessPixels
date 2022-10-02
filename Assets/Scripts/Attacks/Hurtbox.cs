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
        dmgPercent += damage;
        transform.parent.GetComponent<Rigidbody2D>().AddForce(finalForce*((dmgPercent/100)/2));
        Debug.Log(transform.parent);
        GameObject.Find("Canvas").GetComponent<PlayerDmg>().playerProfile[transform.parent.name].transform.Find("dmgPercent").GetComponent<TextMeshProUGUI>().text = dmgPercent+"%";
        return true;

    }

}
