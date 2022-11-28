using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChinchiSpecial : Special
{
    bool done;
    Vector2 i_movement;
    float pSize;
    [SerializeField] GameObject Scope;
    [SerializeField] Misil Misil;
    [SerializeField] Transform ScopeSpawn;
    [SerializeField] Transform FirePoint;
    GameObject scope;
    Misil misil;
    public float speed;

    public override void SpecialStart(PlayerController player)
    {
        scope = Instantiate(Scope, ScopeSpawn.position, Quaternion.identity);
        misil = Instantiate(Misil, FirePoint.position, Quaternion.identity);
        misil.target = scope.transform;
    }

    public override void SpecialUpdate(PlayerController player)
    {
        if(scope != null)
        {
            i_movement = player.i_movement;
            scope.GetComponent<Rigidbody2D>().velocity = i_movement*speed;
        }
    }
}
