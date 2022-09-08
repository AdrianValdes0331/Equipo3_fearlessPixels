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

    public void EscenaGame()
    {
        SceneManager.LoadScene("Game");
    }

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

    public void EscenaEntrenamiento()
    {
        SceneManager.LoadScene("Training");
    }

    public void EscenaStory()
    {
        SceneManager.LoadScene("StoryMode");
    }

    public void ChinchikillerExtras()
    {
        SceneManager.LoadScene("ChinchiKiller");
    }

    public void CustomScene()
    {
        SceneManager.LoadScene("Personalizacion");
    }
}
