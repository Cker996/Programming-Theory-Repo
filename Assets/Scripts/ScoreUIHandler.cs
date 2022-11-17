using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUIHandler : UIHandler
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI arrowText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreArrowText;
    // Start is called before the first frame update
    void Start()
    {
        ScoreUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ScoreUpdate()
    {
        string nameOutput = "";
        string arrowOutput = "";
        string scoreOutput = "";
        string scoreArrowOutput = "";
        if(LoadManager.Instance != null)
        {
            for (int i = 1; i < LoadManager.Instance.playerData.Count; i++)
            {
                nameOutput += string.Format("{0:D2}.  {1}\n", i, LoadManager.Instance.playerData[i].name);
                arrowOutput += string.Format("{0}\n", LoadManager.Instance.playerData[i].arrowNum);
                scoreOutput += string.Format("{0}\n", LoadManager.Instance.playerData[i].score);
                scoreArrowOutput += string.Format("{0}\n", LoadManager.Instance.playerData[i].scoreArrow);
            }
            nameText.text = nameOutput;
            arrowText.text = arrowOutput;
            scoreText.text = scoreOutput;
            scoreArrowText.text = scoreArrowOutput;
        }
        else
        {
            Debug.Log("LoadManager error.");
        }
    }
}
