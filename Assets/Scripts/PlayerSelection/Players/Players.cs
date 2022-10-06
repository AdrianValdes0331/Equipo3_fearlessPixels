using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayer", menuName = "player")]
public class Players : ScriptableObject
{
    public GameObject playablePlayer;
    public Sprite image;
    public new string name;
}
