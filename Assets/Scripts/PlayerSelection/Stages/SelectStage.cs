using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class SelectStage : MonoBehaviour
{
    public static SelectStage Instance;
    public List<Stage> stages;
    // Start is called before the first frame update

    private void Awake()
    {
        if (SelectStage.Instance == null)
        {
            SelectStage.Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
