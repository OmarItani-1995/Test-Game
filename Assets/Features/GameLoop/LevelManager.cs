using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int Rows;
    public int Columns;

    private IGrid grid;
    void Start()
    {
        grid = DI.Get<IGrid>();
        Msg.RegisterListener(typeof(Msg_GameStarted), OnGameStarted);
    }

    private void OnGameStarted(Message message)
    {
        Msg_GameStarted msg = message as Msg_GameStarted;
        Rows = msg.rows;
        Columns = msg.columns;
        grid.GenerateGrid(Rows, Columns);
    }
}
