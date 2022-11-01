using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeModal : MonoBehaviour
{
    public static bool GameModal = false;

    public GameObject codeMenuUI;

    // Update is called once per frame
    public void Abrido()
    {
        Pause();
    }

    public void Cerrado()
    {
        Resume();
    }

    public void Resume()
    {
        codeMenuUI.SetActive(false);
        GameModal = false;
    }

    public void Pause()
    {
        codeMenuUI.SetActive(true);
        GameModal = true;
    }
}
