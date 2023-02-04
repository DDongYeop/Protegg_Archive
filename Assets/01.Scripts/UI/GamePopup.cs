using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePopup : MonoBehaviour
{
    [SerializeField] private GameObject _escPanel;
    [SerializeField] private bool _isPause = false;
    
    public void SettingButtonDown()
    {
        if (GameManager.Instance.gameState != GameState.Game)
            return;
        
        GameManager.Instance.gameState = GameState.EscPopup;
        PanelShow();
    }

    public void PanelShow()
    {
        _escPanel.SetActive(true);
        if (_isPause)
            Time.timeScale = 0;
    }

    public void PanelUnshow()
    {
        if (_isPause)
            Time.timeScale = 1;
        _escPanel.SetActive(false);
    }

    public void MainButton()
    {
        SaveSystem.Instance.Save();
        SceneManager.LoadScene(0);
    }

    public void BackButton()
    {
        Application.Quit();
    }
}
