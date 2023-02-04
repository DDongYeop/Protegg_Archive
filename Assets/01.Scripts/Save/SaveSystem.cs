using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem Instance;
    private string _savePath;
    private string _saveFileName = "/SaveFile.txt";
    public SaveData saveData;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
        
        saveData = new SaveData();
        #if UNITY_EDITOR
        _savePath = Application.dataPath + "/SaveData";
        #else
        _savePath = Application.persistentDataPath + "/SaveData";
        #endif

        if (!Directory.Exists(_savePath))
            Directory.CreateDirectory(_savePath);
    }
    
    // ReSharper disable Unity.PerformanceAnalysis
    public void Save()
    {
        saveData.language = LocalizationSettings.SelectedLocale.ToString();
        Sound sound = FindObjectOfType<Sound>();
        saveData.BGM = sound.bgm;
        saveData.Effect = sound.effect;

        if (GameManager.Instance.gameState != GameState.Title)
        {
            if (GameManager.Instance.score > saveData.maxScore)
                saveData.maxScore = GameManager.Instance.score;
            saveData.thisScore = GameManager.Instance.score;
        }

        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(_savePath + _saveFileName, json);
    }

    public void Load()
    {
        if (File.Exists(_savePath + _saveFileName))
        {
            string json = File.ReadAllText(_savePath + _saveFileName);
            saveData = JsonUtility.FromJson<SaveData>(json);

            LanguageChange languageChange = FindObjectOfType<LanguageChange>();
            Sound sound = FindObjectOfType<Sound>();
            languageChange.LoadLanguage(saveData.language);
            sound.bgm = saveData.BGM;
            sound.effect = saveData.Effect;
            sound.AudioChange();
            
            if (GameManager.Instance.gameState == GameState.Title)
            {
                GameManager.Instance.socreText.text = saveData.thisScore.ToString();
                GameManager.Instance.bestText.text = saveData.maxScore.ToString();
            }
        }
    }
}
