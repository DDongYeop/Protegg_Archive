using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState gameState = GameState.Title;
    public long score;
    
    [SerializeField] private GamePopup _pausePanel;
    [SerializeField] private GamePopup _gameOverPopup;
    [SerializeField] private GamePopup _rewardPopup;
    public GameObject obstacleManager;
    
    public TextMeshProUGUI socreText;
    public TextMeshProUGUI bestText;
    public GameObject noRewardText;
    private double _backGroundScore;
    public bool isReward = false;

    private void Awake() 
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
        
        socreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        if (gameState == GameState.Title)
            bestText = GameObject.Find("BestText").GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        SaveSystem.Instance.Load();
        
        if (gameState != GameState.Title)
            _pausePanel.PanelUnshow();

        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (gameState == GameState.Game && Input.GetKeyDown(KeyCode.Escape))
        {
            gameState = GameState.EscPopup;
            _pausePanel.PanelShow();
        }
        else if (gameState == GameState.EscPopup && Input.GetKeyDown(KeyCode.Escape))
        {
            _pausePanel.PanelUnshow();
            gameState = GameState.Game;
        }
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        gameState = GameState.Advertisement;
        obstacleManager.SetActive(false);
        _gameOverPopup.PanelShow();
        PlayerRevive();
    }

    public void ScoreUp()
    {
        _backGroundScore += Time.deltaTime;
        score = ((long)(_backGroundScore) * 10);
        socreText.text = score.ToString();
        ChickenAnimation.Instance.ChickenIncubateAnimation(true);
    }

    public void StateChange()
    {
        gameState = GameState.Game;
    }

    public void PlayerRevive()
    {
        int random = Random.Range(0, 100);
        if (isReward || random < 50)
        {
            _rewardPopup.PanelUnshow();
            return;
        }

        _rewardPopup.PanelShow();
    }

    public void Reward()
    {
        obstacleManager.SetActive(true);
        _gameOverPopup.PanelUnshow();
        FindObjectOfType<ObstacleManager>().ObstacleStart();
        gameState = GameState.Game;
    }

    public void CloseAd()
    {
        obstacleManager.SetActive(true);
        
    }
}
