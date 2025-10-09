using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_HolderContainer_Grid : Card_HolderContainer
{
    private IGrid _grid;

    void Start()
    {
        _grid = DI.Get<IGrid>();
    }
    
    public override void InitializeCardHolders(int numberOfHolders)
    {
        var points = _grid.GetPoints();
        foreach (var point in points)
        {
            Debug.Log(point);
            var cardHolder = CreateHolder(point.transform);
            cardHolders.Add(cardHolder);
        }
    }
}


