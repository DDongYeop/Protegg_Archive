using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class StartSceenManager : MonoBehaviour
{
    [SerializeField] private float _moveTime;
    
    [SerializeField] private StartSceenState _startSceenState = StartSceenState.Main;
    private RectTransform _mainUI;
    private GamePopup _gamePopup;
    private bool _isMove = false;
        
        private void Awake()
    {
        _mainUI = GameObject.Find("UI").GetComponent<RectTransform>();
        _gamePopup = GameObject.Find("PopUpManager").GetComponent<GamePopup>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && _startSceenState == StartSceenState.Credit)
            CreditExitClick();
        else if (Input.GetKeyDown(KeyCode.Escape) && _startSceenState == StartSceenState.Setting)
            SettingExitClick();
        else if (Input.GetKeyDown(KeyCode.Escape) && _startSceenState == StartSceenState.Main)
        {
            _gamePopup.PanelShow();
            _startSceenState = StartSceenState.EscPanel;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && _startSceenState == StartSceenState.EscPanel)
        {
            _gamePopup.PanelUnshow();;
            _startSceenState = StartSceenState.Main;
        }
    }

    public void StateChange()
    {
        _startSceenState = StartSceenState.Main;
    }

    public void StartClick()
    {
        if (_startSceenState != StartSceenState.Main)
            return;

        SceneManager.LoadScene(1);
    }

    public void CreditClick()
    {
        if (_startSceenState != StartSceenState.Main)
            return;
        
        UIMovement(new Vector2(1080, 0), StartSceenState.Credit);
    }

    public void CreditExitClick()
    {
        if (_startSceenState != StartSceenState.Credit)
            return;
        
        UIMovement(new Vector2(0, 0), StartSceenState.Main);
    }

    public void SettingClick()
    {
        if (_startSceenState != StartSceenState.Main)
            return;
        
        UIMovement(new Vector2(-1080, 0), StartSceenState.Setting);
    }

    public void SettingExitClick()
    {
        if (_startSceenState != StartSceenState.Setting)
            return;
        
        UIMovement(new Vector2(0, 0), StartSceenState.Main);
    }

    public void ExitClick()
    {
        if (_startSceenState != StartSceenState.Main)
            return;
        
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    private void UIMovement(Vector2 pos, StartSceenState startSceenState)
    {
        _isMove = true;
        
        Sequence sequence = DOTween.Sequence();
        sequence.Append(_mainUI.DOAnchorPos(pos, _moveTime));
        sequence.AppendCallback(() =>
        {
            _startSceenState = startSceenState;
            _isMove = false;
        });
    }
}
