using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DisplayCharacter : MonoBehaviour
{
    [HideInInspector] public int index;
    [HideInInspector] [SerializeField] public Image image;
    [HideInInspector] [SerializeField] public new TextMeshProUGUI name;
    [HideInInspector] public SelectPlayers selectPlayers;

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
        index = 2;
        ChangeScreen();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Training");
    }
}
