using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using TMPro;

public class RankingSystem : MonoBehaviour
{
    [SerializeField] private GameObject _googleAccount;
    
    private void Start()
    {
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        LogIn();
    }

    public void LogIn()
    {
        Social.localUser.Authenticate((bool success) =>
        {
            if (!success)
            {
                Debug.Log("Login Fail");
            }
            else
            {
                Debug.Log("Login Success");
            }
        });
    }

    public void ShowLeaderboardUI()
    {
        LogIn();
        UpdateGoogleScore();
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                Social.ShowLeaderboardUI();
                _googleAccount.SetActive(false);
            }
            else
                _googleAccount.SetActive(true);
        });
    }

    public void UpdateGoogleScore()
    {
        Social.ReportScore(SaveSystem.Instance.saveData.thisScore, GPGSIds.leaderboard_score, (bool success) => { });
    }
}
