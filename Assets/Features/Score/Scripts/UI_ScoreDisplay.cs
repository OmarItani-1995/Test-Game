using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_ScoreDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ScoreText;
    
    [Header("Color Settings")]
    [SerializeField] private Color IncreaseColor;
    [SerializeField] private Color DecreaseColor;
    [SerializeField] private Color DefaultColor;
    [SerializeField] private float ColorLerpTime = 0.5f;
    
    [Header("Scale Settings")]
    [SerializeField] private Vector3 IncreaseScale = Vector3.one * 1.2f;
    [SerializeField] private Vector3 DecreaseScale = Vector3.one * 0.8f;
    [SerializeField] private float  ScaleLerpTime = 0.5f;
    
    private ColorLerp _colorLerp = new ColorLerp();
    private Vector3Lerp _scaleLerp = new Vector3Lerp();
    
    private int currentValue;
    public void UpdateDisplay(int newValue)
    {
        if (newValue > currentValue)
        {
            _colorLerp.Start(ScoreText.color, IncreaseColor, DefaultColor, ColorLerpTime, SetColor);
            _scaleLerp.Start(ScoreText.transform.localScale, IncreaseScale, Vector3.one, ScaleLerpTime, SetScale);
        }
        else if (newValue < currentValue)
        {
            _colorLerp.Start(ScoreText.color, DecreaseColor, DefaultColor, ColorLerpTime, SetColor);
            _scaleLerp.Start(ScoreText.transform.localScale, DecreaseScale, Vector3.one, ScaleLerpTime, SetScale);
        }
        currentValue = newValue;        
        ScoreText.text = currentValue.ToString();
    }

    void Update()
    {
        _colorLerp.Update();
        _scaleLerp.Update();
    }
    
    private void SetScale(Vector3 obj)
    {
        ScoreText.transform.localScale = obj;
    }

    private void SetColor(Color obj)
    {
        ScoreText.color = obj;
    }
}