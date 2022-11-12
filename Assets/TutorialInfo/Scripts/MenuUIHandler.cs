using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuUIHandler : MonoBehaviour
{
    public TextMeshProUGUI score;
    public TMP_InputField playerName;
    public TextMeshProUGUI abc;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene("Main");
    }

    public void LoadScoreScene()
    {
        SceneManager.LoadScene("Score");
    }

    public void LoadConfigScene()
    {
        SceneManager.LoadScene("Config");
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
       Application.Quit();
#endif
    }
}
