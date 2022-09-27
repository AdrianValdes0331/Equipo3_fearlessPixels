using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CharacterSelectionMenu : MonoBehaviour
{
    private int index;
    private int PlayerNum = 1;
    [SerializeField] private Image image;
    [SerializeField] private new TextMeshProUGUI name;
    [SerializeField] private Image image2;
    [SerializeField] private new TextMeshProUGUI name2;
    private SelectPlayers selectPlayers;

    private void Start()
    {
        selectPlayers = SelectPlayers.Instance;
        index = PlayerPrefs.GetInt("PlayerIndex");
        if (index > selectPlayers.players.Count - 1)
        {
            index = 0;
        }
        ChangeScreenP1();
    }

    private void ChangeScreenP1()
    {
        PlayerPrefs.SetInt("PlayerIndex", index);
        image.sprite = selectPlayers.players[index].image;
        name.text = selectPlayers.players[index].name;
    }

    private void ChangeScreenP2()
    {
        PlayerPrefs.SetInt("PlayerIndex2", index);
        image2.sprite = selectPlayers.players[index].image;
        name2.text = selectPlayers.players[index].name;
    }

    public void SelectP1()
    {
        PlayerNum = 1;
    }

    public void SelectP2()
    {
        PlayerNum = 2;
    }

    public void SelectChinchikiller()
    {
        index = 1;
        if (PlayerNum == 1)
        {
            ChangeScreenP1();
        }
        else if (PlayerNum == 2)
        {
            ChangeScreenP2();
        }
    }

    public void SelectCowhuahua()
    {
        index = 0;
        if (PlayerNum == 1)
        {
            ChangeScreenP1();
        }
        else if (PlayerNum == 2)
        {
            ChangeScreenP2();
        }
    }
    
    public void StartGame()
    {
        SceneManager.LoadScene("ChinchikillerStage");
    }

    public void StartTraining()
    {
        SceneManager.LoadScene("Training");
    }
}
