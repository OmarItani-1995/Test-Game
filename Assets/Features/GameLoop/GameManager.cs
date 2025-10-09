using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IGameStarter, IGameEnder, IGameRestarter, IGameProgression
{
    void Awake()
    {
        DI.Register<IGameStarter>(this);
        DI.Register<IGameEnder>(this);
        DI.Register<IGameRestarter>(this);
        DI.Register<IGameProgression>(this);
    }

    private ISaveLoad _saveLoad;
    private int _currentLevel = 1;
    void Start()
    {
        _saveLoad = DI.Get<ISaveLoad>();
        _currentLevel = _saveLoad.LoadInt("currentLevel", 1);
    }
    
    public void StartGame(int rows, int columns)
    {
        Msg.TriggerMessage(new Msg_GameStarted()
        {
            level = _currentLevel,
            rows = rows,
            columns = columns
        });
    }

    public void EndGame()
    {
        _saveLoad.SaveInt("currentLevel", _currentLevel + 1);
        Msg.TriggerMessage(new Msg_GameEnded());
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public int CurrentLevel { get { return _currentLevel; } }
}

public class Msg_GameStarted : Message
{
    public int level;
    public int rows;
    public int columns;
}

public class Msg_GameEnded : Message
{
    
}

public interface IGameStarter
{
    void StartGame(int rows, int columns);
}

public interface IGameEnder
{
    void EndGame();
}

public interface IGameRestarter
{
    void RestartGame();
}

public interface IGameProgression
{
    int CurrentLevel { get; }
}