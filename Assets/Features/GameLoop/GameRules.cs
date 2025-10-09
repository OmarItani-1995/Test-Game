using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRules : MonoBehaviour, IGameRules_Layout
{
    void Awake()
    {
        DI.Register<IGameRules_Layout>(this);
    }
    public bool IsRowAndColumnValid(int row, int column)
    {
        return (row * column) % 2 == 0;
    }

    public int DefaultRows { get; set; } = 2;
    public int DefaultColumns { get; set; } = 2;
}

public interface IGameRules_Layout
{
    bool IsRowAndColumnValid(int row, int column);
    int DefaultRows { get; set; }
    int DefaultColumns { get; set; }
}
