using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NoPlayersHurtbox : MonoBehaviour
{

    private Collider2D hcollider;
    public float dmgPercent = 50;

    public bool getHitBy(float damage, int force, int angle, float xPos)
    {
        //alreveza el angulo dependiendo si el ataque esta a la derecha o izquierda
        if (transform.position.x - xPos < 0) { angle = 180 - angle; }
        float radian = angle * Mathf.Deg2Rad;
        Vector2 finalForce = new Vector2(Mathf.Cos(radian), Mathf.Sin(radian)) * force;
        print("Collider: " + gameObject.name);
        print("Fuerza base = " + force);
        print("angulo = " + angle);
        print("Fuerza final = " + finalForce);
        transform.parent.GetComponent<Rigidbody>().AddForce(finalForce * ((dmgPercent / 100) / 2));
        return true;
    }
}
