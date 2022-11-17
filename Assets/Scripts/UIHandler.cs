using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// INHERITANCE
public class UIHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void LoadMainScene()
    {
        SceneManager.LoadScene("Main");
    }

    public virtual void LoadScoreScene()
    {
        SceneManager.LoadScene("Score");
    }

    public virtual void LoadConfigScene()
    {
        SceneManager.LoadScene("Config");
    }

    public virtual void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public virtual void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
