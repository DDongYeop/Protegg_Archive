using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Sound : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Sprite _soundOn;
    [SerializeField] private Sprite _soundOff;
    private Image _bgmSpriteRenderer;
    private Image _effectSpriteRenderer;
    public bool bgm = true;
    public bool effect = true;

    private void Awake()
    {
        _bgmSpriteRenderer = GameObject.Find("BGMButton").GetComponent<Image>();
        _effectSpriteRenderer = GameObject.Find("EffectButton").GetComponent<Image>();
    }

    public void BGMButtonDown()
    {
        bgm = !bgm;
        AudioChange();
    }

    public void EffectButtonDown()
    {
        effect = !effect;
        AudioChange();
    }

    public void AudioChange()
    {
        if (bgm)
        {
            _audioMixer.SetFloat("BGMVolume", 0);
            _bgmSpriteRenderer.sprite = _soundOn;
        }
        else
        {
            _audioMixer.SetFloat("BGMVolume", -80);
            _bgmSpriteRenderer.sprite = _soundOff;
        }
        if (effect)
        {
            _audioMixer.SetFloat("EffectVolume", 0);
            _effectSpriteRenderer.sprite = _soundOn;
        }
        else
        {
            _audioMixer.SetFloat("EffectVolume", -80);
            _effectSpriteRenderer.sprite = _soundOff;
        }
        SaveSystem.Instance.Save();
    }
}
