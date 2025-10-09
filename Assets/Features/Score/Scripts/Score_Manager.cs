using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score_Manager : MonoBehaviour, IScore
{
    private IGameRules_Score _gameRulesScore;

    private int _score;
    private Action<int> _scoreChanged;
    public int Score
    {
        get => _score;
        private set
        {
            if (_score != value)
            {
                _score = value;
                _scoreChanged?.Invoke(_score);
            }
        }
    }


    private int _combo;
    private Action<int> _comboChanged;

    public int Combo
    {
        get => _combo;
        private set
        {
            if (_combo != value)
            {
                _combo = value;
                _comboChanged?.Invoke(_combo);
            }
        }
    }

    void Awake()
    {
        DI.Register<IScore>(this);
    }
    
    void Start()
    {
        _gameRulesScore = DI.Get<IGameRules_Score>();
        
        Msg.RegisterListener(typeof(Msg_MatchHappened), OnMatchHappened);
        Msg.RegisterListener(typeof(Msg_MatchMissed), OnMatchMissed);
    }
    
    public void SubscribeToComboChanged(Action<int> onComboChanged)
    {
        _comboChanged += onComboChanged;
    }
    public void SubscribeToScoreChanged(Action<int> onScoreSchanged)
    {
        _scoreChanged += onScoreSchanged;
    }
    private void OnMatchHappened(Message message)
    {
        Combo++;
        Score += _gameRulesScore.PointsPerMatch * (_gameRulesScore.MultiplyPointsByCombo ? _combo : 1);
    }
    
    private void OnMatchMissed(Message message)
    {
        Combo = 0;
        Score -= _gameRulesScore.PointsLostPerMiss;
        if (Score < 0) Score = 0;        
    }
}

public interface IScore
{
    int Score { get; }
    void SubscribeToScoreChanged(Action<int> onScoreSchanged);
    
    int Combo { get; }
    void SubscribeToComboChanged(Action<int> onComboChanged);
}