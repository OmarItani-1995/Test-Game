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
    
    protected override void OnInitializeCardHolders(int numberOfHolders)
    {
        var points = _grid.GetPoints();
        foreach (var point in points)
        {
            var cardHolder = CreateHolder(point.transform);
            cardHolders.Add(cardHolder);
        }
    }
}


