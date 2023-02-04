using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using TMPro;

public class RankingSystem : MonoBehaviour
{
    private void Start()
    {
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        LogIn();
        
        UpdateGoogleScore();
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
        Social.ShowLeaderboardUI();
        // ((PlayGamesPlatform)Social.Active).ShowLeaderboardUI(GPGSIds.leaderboard_score);
    }

    public void UpdateGoogleScore()
    {
        Social.ReportScore(SaveSystem.Instance.saveData.thisScore, GPGSIds.leaderboard_score, (bool success) => { });
    }
}
