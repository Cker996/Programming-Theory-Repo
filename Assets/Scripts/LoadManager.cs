using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class LoadManager : MonoBehaviour
{
    public static LoadManager Instance { get; private set; }
    public static bool IsLoaded = false;
    public List<PlayerData> playerData = new List<PlayerData>();
    public ConfigData config;
    public PlayerData player;
    public PlayerData bestPlayer;

    // Start is called before the first frame update
    private void Awake()
    {
        if (!IsLoaded)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            IsLoaded = true;
        }
        InitConfig();
        InitData();
        Screen.fullScreen = config.fullScreen;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool InitData()
    {
        string path = Application.persistentDataPath + "/DataSave.bin";
        try
        {
            if (File.Exists(path))
            {
                LoadDataBinary(path);
                if(playerData.Count > 0)
                {
                    player.name = playerData[0].name;
                    player.arrowNum = config.ammo;
                    player.score = 0;
                    player.scoreArrow = 0;
                }
                if (playerData.Count > 1)
                {
                    bestPlayer = playerData[1];
                }
                if (player.name != "")
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else
            {
                return false;
            }
        }
        finally
        {
            if(player.name == "")
            {
                player.name = "Player";
                player.arrowNum = config.ammo;
                playerData.Add(new PlayerData { name = player.name, arrowNum = player.arrowNum });
            }
            if(bestPlayer.name == "")
            {
                bestPlayer.name = "First";
            }
        }
    }

    bool InitConfig()
    {
        string path = Application.persistentDataPath + "/Config.json";
        try
        {
            if (File.Exists(path))
            {
                LoadConfig(path);
                if (config.difficultyLevel != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                    
            }
            else
            {
                return false;
            }
        }
        finally
        {
            if (config.difficultyLevel == 0)
            {
                config.difficultyLevel = 1;
                config.ammo = 20;
                config.generateNum = 3;
                config.regenerateSec = 5;
                config.fullScreen = true;
                SaveConfig();
            }
        }
    }

    public void RefreshName(string name)
    {
        player.name = name;
        playerData[0].name = name;
    }

    public void RefreshScore()
    {
        playerData[0].arrowNum = config.ammo;
        playerData[0].score = player.score;
        playerData[0].scoreArrow = (float)playerData[0].score / (float)playerData[0].arrowNum;
        playerData.Sort(delegate (PlayerData x, PlayerData y)
        {
            if (x.scoreArrow < y.scoreArrow) return 1;
            else if (x.scoreArrow == y.scoreArrow) return 0;
            else return -1;

        });
        for(int i = 16; i < playerData.Count; i++)
        {
            playerData.RemoveAt(i);
        }
        player.score = 0;
        player.scoreArrow = 0;
        playerData.Insert(0, new PlayerData {name = player.name, arrowNum = player.arrowNum});
        bestPlayer = playerData[1];
    }

    public void LoadConfig(string pathC)
    {
        string json = File.ReadAllText(pathC);
        config = JsonUtility.FromJson<ConfigData>(json);
    }

    public void SaveConfig()
    {
        if(config.difficultyLevel > 0)
        {
            string json = JsonUtility.ToJson(config);
            File.WriteAllText(Application.persistentDataPath + "/Config.json", json);
        }
        else
        {
            Debug.Log("Config didn't set");
        }

    }

    public void SaveDataBinary()
    {
        string json = "";
        foreach(PlayerData data in playerData)
        {
            json += JsonUtility.ToJson(data) + "\n";
        }
        json = json.TrimEnd('\n');
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(Application.persistentDataPath + "/DataSave.bin", FileMode.Create, FileAccess.Write, FileShare.None);
        formatter.Serialize(stream, json);
        stream.Close();
    }

    void LoadDataBinary(string paths)
    {
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(paths, FileMode.Open, FileAccess.Read, FileShare.Read);
        string fileA = (string)formatter.Deserialize(stream);
        stream.Close();
        string[] subfile = fileA.Split('\n');
        foreach(var sub in subfile)
        {
            playerData.Add(JsonUtility.FromJson<PlayerData>(sub));
        }
    }

    [System.Serializable]
    public class PlayerData
    {
        public float scoreArrow;
        public int arrowNum;
        public int score;
        public string name;
    }

    [System.Serializable]
    public class ConfigData
    {
        public int difficultyLevel;
        public int ammo;
        public int generateNum;
        public float regenerateSec;
        public bool fullScreen;
    }
}
