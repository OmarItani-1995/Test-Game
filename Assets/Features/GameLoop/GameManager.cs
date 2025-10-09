using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IGameStarter, IGameEnder, IGameRestarter
{
    void Awake()
    {
        DI.Register<IGameStarter>(this);
        DI.Register<IGameEnder>(this);
        DI.Register<IGameRestarter>(this);
    }
    
    public void StartGame(int rows, int columns)
    {
        Msg.TriggerMessage(new Msg_GameStarted()
        {
            rows = rows,
            columns = columns
        });
    }

    public void EndGame()
    {
        Msg.TriggerMessage(new Msg_GameEnded());
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}

public class Msg_GameStarted : Message
{
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