using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuUIHandler : UIHandler
{
    public TextMeshProUGUI score;
    public TMP_InputField playerName;
    // Start is called before the first frame update
    void Start()
    {
        if (LoadManager.Instance != null)
        {
            playerName.text = LoadManager.Instance.player.name;
            score.text = "Best Score: " + LoadManager.Instance.bestPlayer.name + "'s " + LoadManager.Instance.bestPlayer.scoreArrow + " point.";
        }
        else
        {
            Debug.Log("Can't load player name.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void LoadMainScene()
    {
        if (LoadManager.Instance != null)
        {
            LoadManager.Instance.RefreshName(playerName.text);
        }
        base.LoadMainScene();
    }

    public void ExitGame()
    {
        if (LoadManager.Instance != null)
        {
            LoadManager.Instance.SaveDataBinary();
        }
        
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
       Application.Quit();
#endif
    }
}
