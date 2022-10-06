using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class SelectPlayers : MonoBehaviour
{
    public static SelectPlayers Instance;
    public List<Players> players;
    // Start is called before the first frame update
    
    private void Awake()
    {
        if (SelectPlayers.Instance == null)
        {
            SelectPlayers.Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
