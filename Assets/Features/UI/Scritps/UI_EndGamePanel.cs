using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_EndGamePanel : MonoBehaviour
{
    [SerializeField] private UI_Manager uiManager;
    
    public void RestartGame()
    {
        uiManager.RestartGame();
    }
}
