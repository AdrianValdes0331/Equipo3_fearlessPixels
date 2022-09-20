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

    public void doExitGame()
    {
        Application.Quit();
    }
}
