using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PlayerBaseState
{
    public void EnterState(PlayerController player);
    public void Update(PlayerController player);
    public void OnTriggerEnter(PlayerController player, Collider2D col);
    public void OnCollisionEnter(PlayerController player, Collider2D col);
}
