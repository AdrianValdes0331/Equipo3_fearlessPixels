using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitFight : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     
        if (Input.GetKeyDown(KeyCode.Escape)){
            GameObject[] splayers = GameObject.FindGameObjectsWithTag("SPlayes");
            foreach (GameObject splay in splayers)
            {
                GameObject.Destroy(splay);
            }

            //Destroy network manager
            if (NetworkManager.instance != null)
            {
                PhotonNetwork.LeaveRoom();
                NetworkManager.instance.DestroyBeforeLeave();
            }

            SceneManager.LoadScene("MainMenu");
        }

    }
}
