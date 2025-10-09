using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, IGameStarter
{
    void Awake()
    {
        DI.Register<IGameStarter>(this);
    }
    public void StartGame(int rows, int columns)
    {
        Msg.TriggerMessage(new Msg_StartGame()
        {
            rows = rows,
            columns = columns
        });
    }
}

public class Msg_StartGame : Message
{
    public int rows;
    public int columns;
}

public interface IGameStarter
{
    void StartGame(int rows, int columns);
}