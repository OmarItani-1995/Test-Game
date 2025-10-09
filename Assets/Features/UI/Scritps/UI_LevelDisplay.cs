using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_LevelDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelText;
    private IGameProgression _gameProgression;
    
    void Start()
    {
        _gameProgression = DI.Get<IGameProgression>();
        SetText(_gameProgression.CurrentLevel);
    }
    
    private void SetText(int level)
    {
        levelText.text = level.ToString();
    }
}
