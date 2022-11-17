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
    [SerializeField] private TextMeshProUGUI name2;
    [SerializeField] private Image image3;
    [SerializeField] private TextMeshProUGUI name3;
    [SerializeField] private Image image4;
    [SerializeField] private TextMeshProUGUI name4;
    private SelectPlayers selectPlayers;

    private void Start()
    {
        selectPlayers = SelectPlayers.Instance;
        ChangeScreenNULL();
        /*index = PlayerPrefs.GetInt("PlayerIndex");
        if (index > selectPlayers.players.Count - 1)
        {
            index = 0;
        }
        ChangeScreenP1();*/
    }
    public void Update()
    {
        int playerIndex = PlayerPrefs.GetInt("PlayerIndex");
        int player2Index = PlayerPrefs.GetInt("PlayerIndex2");
        int player3Index = PlayerPrefs.GetInt("PlayerIndex3");
        int player4Index = PlayerPrefs.GetInt("PlayerIndex4");
        if ((playerIndex + player2Index + player3Index != 18 && playerIndex + player2Index + player4Index != 18 &&
            player4Index + player2Index + player3Index != 18 && playerIndex + player3Index + player4Index != 18))
        {
            GameObject.Find("StartButton").transform.GetComponent<Fader>().enabled = true;
        }
        else
        {
            GameObject.Find("StartButton").transform.GetComponent<Fader>().enabled = false;
        }

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

    private void ChangeScreenP3()
    {
        PlayerPrefs.SetInt("PlayerIndex3", index);
        image3.sprite = selectPlayers.players[index].image;
        name3.text = selectPlayers.players[index].name;
        Debug.Log(index);
    }

    private void ChangeScreenP4()
    {
        PlayerPrefs.SetInt("PlayerIndex4", index);
        image4.sprite = selectPlayers.players[index].image;
        name4.text = selectPlayers.players[index].name;
    }

    private void ChangeScreenCPU()
    {
        PlayerPrefs.SetInt("PlayerIndex5", index);      
    }

    private void ChangeScreenNULL()
    {
        index = 6;
        PlayerPrefs.SetInt("PlayerIndex", index);
        PlayerPrefs.SetInt("PlayerIndex2", index);
        PlayerPrefs.SetInt("PlayerIndex3", index);
        PlayerPrefs.SetInt("PlayerIndex4", index);
    }

    public void SelectP1()
    {
        PlayerNum = 1;
    }

    public void SelectP2()
    {
        PlayerNum = 2;
    }

    public void SelectP3()
    {
        PlayerNum = 3;
    }

    public void SelectP4()
    {
        PlayerNum = 4;
    }
    public void SelectP5()
    {
        PlayerNum = 5;
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
        else if (PlayerNum == 3)
        {
            ChangeScreenP3();
        }
        else if (PlayerNum == 4)
        {
            ChangeScreenP4();
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
        else if (PlayerNum == 3)
        {
            ChangeScreenP3();
        }
        else if (PlayerNum == 4)
        {
            ChangeScreenP4();
        }
    }

    public void SelectCPU()
    {
        index = 5;
        if (PlayerNum == 1)
        {
            ChangeScreenP1();
        }
        else if (PlayerNum == 2)
        {
            ChangeScreenP2();
        }
        else if (PlayerNum == 3)
        {
            ChangeScreenP3();
        }
        else if (PlayerNum == 4)
        {
            ChangeScreenP4();
        }
        else if (PlayerNum == 5)
        {
            ChangeScreenCPU();
        }
    }

        public void SelectFoxHunter()
    {
        index = 2;
        if (PlayerNum == 1)
        {
            ChangeScreenP1();
        }
        else if (PlayerNum == 2)
        {
            ChangeScreenP2();
        }
        else if (PlayerNum == 3)
        {
            ChangeScreenP3();
        }
        else if (PlayerNum == 4)
        {
            ChangeScreenP4();
        }
    }

    public void SelectBrujorge()
    {
        index = 3;
        if (PlayerNum == 1)
        {
            ChangeScreenP1();
        }
        else if (PlayerNum == 2)
        {
            ChangeScreenP2();
        }
        else if (PlayerNum == 3)
        {
            ChangeScreenP3();
        }
        else if (PlayerNum == 4)
        {
            ChangeScreenP4();
        }
    }

    public void StartGame()
    {
        int playerIndex = PlayerPrefs.GetInt("PlayerIndex");
        int player2Index = PlayerPrefs.GetInt("PlayerIndex2");
        int player3Index = PlayerPrefs.GetInt("PlayerIndex3");
        int player4Index = PlayerPrefs.GetInt("PlayerIndex4");
        if ((playerIndex + player2Index + player3Index != 18 && playerIndex + player2Index + player4Index != 18 &&
            player4Index + player2Index + player3Index != 18 && playerIndex + player3Index + player4Index != 18))
        {
            Time.timeScale = 1f;
            int stageIndex = PlayerPrefs.GetInt("StageIndex");
            Debug.Log(stageIndex);
            SceneManager.LoadScene(SelectStage.Instance.stages[stageIndex].StageName);
        }
        Debug.Log("Not Enough Players");
    }

    public void StartTraining()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Training");
    }
}
