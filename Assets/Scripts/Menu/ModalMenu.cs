using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModalMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

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
        pauseMenuUI.SetActive(false);
        GameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        GameIsPaused = true;
    }
}
