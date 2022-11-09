using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EscenaGameHost()
    {
        PlayerPrefs.SetInt("isOnline", 1);
        SceneManager.LoadScene("GameHost");
    }

    /*public void EscenaGame()
    {
        SceneManager.LoadScene("FreePlay");
    }*/

    public void EscenaMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void EscenaOptions()
    {
        SceneManager.LoadScene("Options");
    }

    public void EscenaExtras()
    {
        SceneManager.LoadScene("Extras");
    }

    public void EscenaFreePlaySeleccionStage()
    {
        PlayerPrefs.SetInt("isOnline", 0);
        SceneManager.LoadScene("StageSelect");
    }

    public void EscenaFreePlaySeleccionPersonajes()
    {
        SceneManager.LoadScene("CharacterSelect");
    }

    public void EscenaEntrenamientoSeleccionPersonajes()
    {
        SceneManager.LoadScene("TrainingCharacterSelect");
    }

    public void EscenaStory()
    {
        SceneManager.LoadScene("StoryMode");
    }

    public void ChinchikillerExtras()
    {
        SceneManager.LoadScene("ChinchiKiller");
    }

    public void BrujorgeExtras()
    {
        SceneManager.LoadScene("Brujorge");
    }

    public void CL4R174Extras()
    {
        SceneManager.LoadScene("CL4R174");
    }

    public void CowhuahuaExtras()
    {
        SceneManager.LoadScene("Cowhuahua");
    }

    public void FoxHunterExtras()
    {
        SceneManager.LoadScene("Fox Hunter");
    }

    public void CustomScene()
    {
        SceneManager.LoadScene("Personalizacion");
    }

    public void EscenaMultiplayerSeleccionStage()
    {
        PlayerPrefs.SetInt("MultiplayerType", 0); // 0 para el host
        SceneManager.LoadScene("MultiplayerStageSelect");
    }

    public void EscenaMultiplayerSeleccionPersonajes()
    {
        PlayerPrefs.SetInt("MultiplayerType",  1); // 1 para jugador que se une
        SceneManager.LoadScene("MultiplayerCharacterSelect");
    }

    public void doExitGame()
    {
        Application.Quit();
    }
}
