using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStage", menuName = "stage")]
public class Stage : ScriptableObject
{
    public string StageName;
    public Sprite image;
    public new string name;
}
