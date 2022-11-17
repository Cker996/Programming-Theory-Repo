using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public TextMeshProUGUI scoreText;
    public GameObject arrowPrefab;
    public GameObject[] balloons;

    private UserControl userControlScript;
    private float m_regenerateSec = 5;
    private int m_generateNum = 3;
    private int m_ammo = 20;
    private int m_score = 0;
    private int spawnCycle = 0;
    private bool m_IsAlive = true;
    private bool gameOver = false;

    // ENCAPSULATION
    public bool IsAlive { get { return m_IsAlive; } }
    public int ammo { get { return m_ammo; } }
    public int score { get { return m_score; } }
    public float regenerateSec
    {
        get { return m_regenerateSec; }
        set
        {
            if(regenerateSec > 0.5 && regenerateSec < 60)
            {
                m_regenerateSec = regenerateSec;
            }
            else
            {
                Debug.Log("regenerateSec is too small or too big.");
            }
        }
    }
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        InitConfig();
        userControlScript = GameObject.Find("User Control").GetComponent<UserControl>();
        if(balloons.Length > 0)
        {
            GenerateBalloons();
            StartCoroutine(SpawnBallon());
        }
        else
        {
            Debug.Log("no balloon prefab.");
        }
        scoreText.text = "Score: " + m_score;
    }

    void InitConfig()
    {
        if(LoadManager.Instance != null)
        {
            if (LoadManager.Instance.config.difficultyLevel != 0)
            {
                if(arrowPrefab != null)
                {
                    if(LoadManager.Instance.config.difficultyLevel == 1)
                    {
                        arrowPrefab.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
                    }
                    else
                    {
                        arrowPrefab.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
                    }
                }
                else
                {
                    Debug.Log("arrowPrefab is null.");
                }
                m_regenerateSec = LoadManager.Instance.config.regenerateSec;
                m_generateNum = LoadManager.Instance.config.generateNum;
                m_ammo = LoadManager.Instance.config.ammo;
            }
            else
            {
                Debug.Log("config didn't set.");
            }
        }
        else
        {
            Debug.Log("no LoadManager.");
        }
    }

    IEnumerator SpawnBallon()
    {
        yield return new WaitForSeconds(m_regenerateSec);
        GenerateBalloons();
        if (m_IsAlive)
        {
            StartCoroutine(SpawnBallon());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(userControlScript != null)
        {
            if(userControlScript.CountArrow() < 1)
            {
                m_IsAlive = false;
            }
        }
        else
        {
            Debug.Log("can't find UserControlScript.");
        }
        if (LoadManager.Instance != null)
        {
            LoadManager.Instance.player.score = m_score;
        }
        scoreText.text = "Score: " + m_score;
        if (!IsAlive && !gameOver)
        {
            GameOver();
            gameOver = true;
        }
    }

    void GameOver()
    {
        if(LoadManager.Instance != null)
        {
            LoadManager.Instance.RefreshScore();
            LoadManager.Instance.SaveDataBinary();
        }
        else
        {
            Debug.Log("Can't save data.");
        }
    }

    public void AddScore(int i)
    {
        if(i > 0 && i < 20)
        {
            m_score += i;
        }
        else
        {
            Debug.Log("AddScore wrong number.");
        }
    }

    void GenerateBalloons()
    {

        for (int i = 0; i < m_generateNum; i++)
        {
            int balloonIndex = Random.Range(0, balloons.Length);
            Vector3 generatePos = new Vector3(Random.Range(-30.0f, 30.0f), -10, Random.Range(8.0f, 30.0f));
            Instantiate(balloons[balloonIndex], generatePos, balloons[balloonIndex].transform.rotation);
        }
        spawnCycle++;
        Debug.Log("generate " + spawnCycle + " times, " + m_generateNum + " balloons.");
    }
}
