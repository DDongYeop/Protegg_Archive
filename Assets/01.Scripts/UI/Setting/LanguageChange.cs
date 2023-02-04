using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LanguageChange : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _languageText;
    [SerializeField] private List<string> _languageList;
    public int _languageIndex;

    public void LoadLanguage(string language)
    {
        switch (language)
        {
            case "English (en)":
                _languageIndex = 0;
                break;
            case "Korean (ko)":
                _languageIndex = 1;
                break;
            default:
                LoadLanguage(LocalizationSettings.SelectedLocale.ToString());
                return;
        }
        LanguageSave();
    }

    public void ButtonClick(int index)
    {
        _languageIndex += index;

        if (_languageIndex < 0)
            _languageIndex = _languageList.Count - 1;
        else if (_languageIndex >= _languageList.Count)
            _languageIndex = 0;

        LanguageSave();
    }

    private void LanguageSave()
    {
        StartCoroutine(LanguageChangeCoroutine());
        _languageText.text = _languageList[_languageIndex];
    }

    private IEnumerator LanguageChangeCoroutine()
    {
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_languageIndex];
        SaveSystem.Instance.Save();
    }
}
