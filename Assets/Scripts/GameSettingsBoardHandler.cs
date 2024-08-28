using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameSettingsBoardHandler : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] Button _submitButton;
    [SerializeField] private GameObject spaceButtonLabel;
    [SerializeField] private Slider playerMinimumSpeedSlider;
    [SerializeField] private Slider playerMaximumSpeedSlider;
    [SerializeField] private Slider speedIntervalSlider;

    private void Start()
    {
        _submitButton.onClick.AddListener(SetNamesToGameManager);
    }

    public void SetNamesToGameManager()
    {
        if (String.IsNullOrEmpty(_inputField.text)){ return; }

        GameManager.Instance.playerMinimumSpeed = playerMinimumSpeedSlider.value;
        GameManager.Instance.playerMaximumSpeed = playerMaximumSpeedSlider.value;
        GameManager.Instance.speedChangeInterval = (int) speedIntervalSlider.value;
        GameManager.Instance.PlayerNames = _inputField.text;
        spaceButtonLabel.SetActive(true);
        gameObject.SetActive(false);
        GameManager.Instance.SetupGame();
        GameManager.Instance.isGameReady = true;
        
    }
}
