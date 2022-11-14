using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class LoadManager : MonoBehaviour
{
    public static LoadManager Instance;
    public static bool IsLoaded = false;
    public List<PlayerData> playerData = new List<PlayerData>();



    public string playerName { get; private set; }
    // Start is called before the first frame update
    private void Awake()
    {
        if (!IsLoaded)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            IsLoaded = true;
        }
        InitData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitData()
    {
        string path = Application.persistentDataPath + "DataSave.bin";
        try
        {
            if (File.Exists(path))
            {
                LoadDataBinary(path);
            }
        }
        finally
        {
            if(playerName == "")
            {
                playerName = "Player";
            }
        }
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
        public int score;
        public string name;
    }
}
