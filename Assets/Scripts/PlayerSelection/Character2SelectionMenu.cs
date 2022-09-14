using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Character2SelectionMenu : MonoBehaviour
{
    private int index;
    [SerializeField] private Image image;
    [SerializeField] private new TextMeshProUGUI name;
    private SelectPlayers selectPlayers;

    private void Start()
    {
        selectPlayers = SelectPlayers.Instance;
        index = PlayerPrefs.GetInt("PlayerIndex");
        if (index > selectPlayers.players.Count - 1)
        {
            index = 0;
        }
        ChangeScreen();
    }

    private void ChangeScreen()
    {
        PlayerPrefs.SetInt("PlayerIndex", index);
        image.sprite = selectPlayers.players[index].image;
        name.text = selectPlayers.players[index].name;
    }

    public void SelectChinchikiller()
    {
        index = 1;
        ChangeScreen();
    }

    public void SelectCowhuahua()
    {
        index = 0;
        ChangeScreen();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Training");
    }
}
