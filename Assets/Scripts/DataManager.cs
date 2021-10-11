using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public string playerName;
    public string highscoreName;
    public int highscorePoints;
    private InputField nameField;

    private void Awake()
    {
        nameField = GameObject.Find("NameField").GetComponent<InputField>();

        if (Instance != null)
        {
            Destroy(Instance);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadScore();
    }
    public void InitName()
    {
        playerName = nameField.text;
    }

    [System.Serializable]
    public class SaveData
    {
        public string name;
        public int score;
    }

    public void SaveScore()
    {
        SaveData data = new SaveData();
        data.name = playerName;
        data.score = highscorePoints;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            highscoreName = data.name;
            highscorePoints = data.score;
        }
    }
}
