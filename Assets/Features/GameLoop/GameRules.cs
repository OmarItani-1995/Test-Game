using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Class containing game rules, for prototype it can be class, or a scriptable that redirect fields
/// For a live game it can be a remote values fetched from a server 
/// </summary>
public class GameRules : MonoBehaviour, IGameRules_Layout, IGameRules_Score
{
    void Awake()
    {
        DI.Register<IGameRules_Layout>(this);
        DI.Register<IGameRules_Score>(this);
    }
    public bool IsRowAndColumnValid(int row, int column)
    {
        return (row * column) % 2 == 0;
    }

    public int DefaultRows { get; set; } = 2;
    public int DefaultColumns { get; set; } = 2;
    public int MaxRows { get; set; } = 8;
    public int MaxColumns { get; set; } = 8;
    public int MinRows { get; set; } = 2;
    public int MinColumns { get; set; } = 2;
    public int PointsPerMatch { get; set; } = 3;
    public int PointsLostPerMiss { get; set; } = 1;
    public bool MultiplyPointsByCombo { get; set; } = true;
}

public interface IGameRules_Layout
{
    bool IsRowAndColumnValid(int row, int column);
    int DefaultRows { get; set; }
    int DefaultColumns { get; set; }
    
    int MaxRows { get; set; }
    int MaxColumns { get; set; }
    
    int MinRows { get; set; }
    int MinColumns { get; set; }
}

public interface IGameRules_Score
{
    int PointsPerMatch { get; set; }
    int PointsLostPerMiss { get; set; }
    bool MultiplyPointsByCombo { get; set; }
}