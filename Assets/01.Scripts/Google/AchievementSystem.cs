using System;
using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using UnityEngine;
using UnityEngine.UI;

public class AchievementSystem : MonoBehaviour
{
    public static AchievementSystem Instance;
    [SerializeField] private List<Button> _buttons;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void Achievement()
    {
        for (int i = 0; i < _buttons.Count; i++)
            _buttons[i].interactable = false;
        
        if (SaveSystem.Instance.saveData.maxScore >= 0)
        {
            PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_whitechicken, 1, null);
            _buttons[0].interactable = true;
        }
        if (SaveSystem.Instance.saveData.maxScore >= 500)
        {
            PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_brownchicken, 1, null);
            _buttons[1].interactable = true;
        }
        if (SaveSystem.Instance.saveData.maxScore >= 1000)
        {
            PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_darkchicken, 1, null);
            _buttons[2].interactable = true;
        }
    }

    public void AchievementButtonDown(int index)
    {
        SaveSystem.Instance.saveData.chickenIndex = index;
        SaveSystem.Instance.Save();
        ChickenAnimation.Instance.Load(index);
    }
}
