using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadExtras : MonoBehaviour
{
    static public int esceneType = 0;
    

    public void loadExtrasChinchikiller()
    {
        esceneType = 1;
        SceneManager.LoadScene("charactersExtras");
    }

    public void loadExtrasCL4R174()
    {
        esceneType = 2;
        SceneManager.LoadScene("charactersExtras");
    }

    public void loadExtrasCowawa()
    {
        esceneType = 3;
        SceneManager.LoadScene("charactersExtras");
    }

    public void loadExtrasFoxHunter()
    {
        esceneType = 4;
        SceneManager.LoadScene("charactersExtras");
    }
}
