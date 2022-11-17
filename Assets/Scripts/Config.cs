using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Config : UIHandler
{
    public TMP_InputField ammoText;
    public TMP_InputField generateNumText;
    public TMP_InputField regenerateSecText;
    public GameObject easy;
    public GameObject hard;
    public Toggle fullScreen;

    private int difficultyLevel = 0;
    private int ammo = 20;
    private int generateNum = 3;
    private float regenerateSec = 5.0f;


    // Start is called before the first frame update
    void Awake()
    {
        InitConfig();
    }

    void InitConfig()
    {
        if (LoadManager.Instance != null)
        {
            if (LoadManager.Instance.config.difficultyLevel != 0)
            {
                difficultyLevel = LoadManager.Instance.config.difficultyLevel;
                regenerateSec = LoadManager.Instance.config.regenerateSec;
                generateNum = LoadManager.Instance.config.generateNum;
                ammo = LoadManager.Instance.config.ammo;
                if(difficultyLevel == 1)
                {
                    easy.GetComponent<Image>().color = Color.red;
                    hard.GetComponent<Image>().color = Color.white;
                }
                else if(difficultyLevel == 2)
                {
                    easy.GetComponent<Image>().color = Color.white;
                    hard.GetComponent<Image>().color = Color.red;
                }
            }
            else
            {
                Debug.Log("config didn't set.");
            }
            ammoText.text = ammo.ToString();
            generateNumText.text = generateNum.ToString();
            regenerateSecText.text = regenerateSec.ToString();
        }
        else
        {
            Debug.Log("no LoadManager.");
        }
    }

    private void Update()
    {
        CheckInput();
        CheckButton();
    }

    void CheckButton()
    {
        GameObject obj = EventSystem.current.currentSelectedGameObject;
        if(obj != null)
        {
            if (obj.name == "Easy")
            {
                easy.GetComponent<Image>().color = Color.red;
                hard.GetComponent<Image>().color = Color.white;
                difficultyLevel = 1;
            }
            if (obj.name == "Hard")
            {
                easy.GetComponent<Image>().color = Color.white;
                hard.GetComponent<Image>().color = Color.red;
                difficultyLevel = 2;
            }
        }
    }

    void CheckInput()
    {
        if (ammoText.text == "" || Convert.ToInt32(ammoText.text) < 1 || Convert.ToInt32(ammoText.text) > 1000)
        {
            ammoText.text = "20";
        }
        if (generateNumText.text == "" || Convert.ToInt32(generateNumText.text) < 1 || Convert.ToInt32(generateNumText.text) > 100)
        {
            generateNumText.text = "3";
        }
        if (regenerateSecText.text == "" || Convert.ToInt32(generateNumText.text) < 0.5 || Convert.ToInt32(generateNumText.text) > 60)
        {
            regenerateSecText.text = "5";
        }
        if (difficultyLevel <= 0 || difficultyLevel > 10)
        {
            difficultyLevel = 1;
        }
    }

    public void CheckScreen()
    {
        Screen.fullScreen = fullScreen.isOn;
    }

    void SetConfig()
    {
        ammo = Convert.ToInt32(ammoText.text);
        generateNum = Convert.ToInt32(generateNumText.text);
        regenerateSec = Convert.ToSingle(regenerateSecText.text);
        LoadManager.Instance.config.difficultyLevel = difficultyLevel;
        LoadManager.Instance.config.ammo = ammo;
        LoadManager.Instance.config.generateNum = generateNum;
        LoadManager.Instance.config.regenerateSec = regenerateSec;
        LoadManager.Instance.config.fullScreen = fullScreen.isOn;
        LoadManager.Instance.SaveConfig();
    }

    public override void BackToMenu()
    {
        SetConfig();
        base.BackToMenu();
    }
}
