using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knock_Back : MonoBehaviour
{
    public int health = 100;

    public Transform hitbox;
    public float hitboxRadius = 1.45f;
    public LayerMask damageSource;
    private bool isHit;

    private bool hitCooldown = false;

    public Rigidbody2D rb;
    public float knockBackForce = 10;
    public float knockBackForceUp = 2;

    void Update()
    {
        isHit = Physics2D.OverlapCircle(hitbox.position, hitboxRadius, damageSource);

        if (isHit == true && hitCooldown == false)
        {
            knockBack();

            hitCooldown = true;
            Invoke("Cooldown", 0.8f);
            //health = health - 25;

            if(health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public void knockBack()
    {
        Transform attacker = getClosestDamageSource();
        Vector2 knockbackDirection = new Vector2(transform.position.x - attacker.transform.position.x, 0);
        rb.velocity = new Vector2(knockbackDirection.x, knockBackForceUp) * knockBackForce;
    }

    public Transform getClosestDamageSource()
    {
        GameObject[] DamageSources = GameObject.FindGameObjectsWithTag("WEAPON");
        float closestDistanz = Mathf.Infinity;
        Transform currentClosestDamageSource = null;

        foreach (GameObject go in DamageSources)
        {
            float currentDistanz;
            currentDistanz = Vector3.Distance(transform.position, go.transform.position);
            if(currentDistanz < closestDistanz)
            {
                closestDistanz = currentDistanz;
                currentClosestDamageSource = go.transform;
            }
        }

        return currentClosestDamageSource;
    }

    public void Cooldown()
    {
        hitCooldown = false;
    }
}
