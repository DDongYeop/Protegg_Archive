using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using TMPro;

public class RankingSystem : MonoBehaviour
{
    public static RankingSystem Instance = null;
    [SerializeField] private GameObject _googleAccount;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        LogIn();
    }

    public void LogIn()
    {
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
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

    public void UpdateGoogleScore(long score)
    {
        Social.localUser.Authenticate((bool success) => 
        {
            if(success)
            {
                Debug.Log(Social.localUser.id);
            }
        });
        print(score);
        Social.ReportScore(score, GPGSIds.leaderboard_score, null);
    }
}
