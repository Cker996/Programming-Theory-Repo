using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUIHandler : UIHandler
{
    public GameObject tryAgainText;
    public GameObject tryAgainBtn;

    private GameManager gameManager;
    // Start is called before the first frame update
    void Awake()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckIsAlive();
    }

    void CheckIsAlive()
    {
        if(gameManager != null)
        {
            if (!gameManager.IsAlive)
            {
                tryAgainText.SetActive(true);
                tryAgainBtn.SetActive(true);
            }
        }
        else
        {
            Debug.Log("can't find Game Manager.");
        }
    }
}
