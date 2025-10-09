using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class UI_ScoreManager : MonoBehaviour
{
    [SerializeField] private UI_ScoreDisplay ScoreDisplay;
    [SerializeField] private UI_ScoreDisplay ComboDisplay;
    
    private IScore _scoreManager;
    
    void Start()
    {
        _scoreManager = DI.Get<IScore>();
        _scoreManager.SubscribeToScoreChanged(OnScoreChanged);
        _scoreManager.SubscribeToComboChanged(OnComboChanged);
    }

    private void OnScoreChanged(int obj)
    {
        ScoreDisplay.UpdateDisplay(obj);
    }
    
    private void OnComboChanged(int obj)
    {
        ComboDisplay.UpdateDisplay(obj);
    }
}







