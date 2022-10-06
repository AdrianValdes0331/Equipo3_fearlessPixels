using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class StageSelectionMenu : MonoBehaviour
{
    private int index;
    [SerializeField] private Image image;
    [SerializeField] private new TextMeshProUGUI name;
    private SelectStage selectStage;

    private void Start()
    {
        selectStage = SelectStage.Instance;
        index = PlayerPrefs.GetInt("StageIndex");
        if (index > selectStage.stages.Count - 1)
        {
            index = 0;
        }
        ChangeScreen();
    }

    private void ChangeScreen()
    {
        PlayerPrefs.SetInt("StageIndex", index);
        image.sprite = selectStage.stages[index].image;
        name.text = selectStage.stages[index].name;
    }

    public void SelectStageChinchikiller()
    {
        index = 1;    
    }

    public void SelectStageCowhuahua()
    {
        index = 0;               
    }

    public void StartStageGame()
    {
        SceneManager.LoadScene("CharacterSelect");
    }

    public void StartStageTraining()
    {
        SceneManager.LoadScene("TrainingCharacterSelect");
    }
}
