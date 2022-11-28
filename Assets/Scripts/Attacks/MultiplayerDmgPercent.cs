using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MultiplayerDmgPercent : MonoBehaviour
{

    public TextMeshProUGUI dmgPlayer;
    public GameObject DmgManager;

    // Start is called before the first frame update
    void Start()
    {
        DmgManager = GameObject.Find("DmgManager");
        dmgPlayer = DmgManager.GetComponent<DmgManager>().dmgPlayer1;
        dmgPlayer.text = "10%";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
