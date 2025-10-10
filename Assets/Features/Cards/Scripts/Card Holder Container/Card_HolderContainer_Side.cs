using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_HolderContainer_Side : Card_HolderContainer
{
    protected override void OnInitializeCardHolders(int numberOfHolders)
    {
        var minimumPoint = DI.Get<IGrid>().GetMinimumPoint();
        transform.position = new Vector3(Mathf.Min(minimumPoint.x * 2, -5f), 0, 0);
        
        for (int i = 0; i < numberOfHolders; i++)
        {
            cardHolders.Add(CreateHolder(transform));
        }
    }
}
