using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] private List<GameObject> uiScreens;

    private IGameStarter _gameStarter;
    void Start()
    {
        _gameStarter = DI.Get<IGameStarter>();
        EnableUI(0);
    }

    public void StartGame(int rows, int columns)
    {
        _gameStarter.StartGame(rows, columns);
        EnableUI(1);
    }

    void EnableUI(int index)
    {
        for (int i = 0 ; i < uiScreens.Count ; i++)
            uiScreens[i].SetActive(i == index);
    }
}
