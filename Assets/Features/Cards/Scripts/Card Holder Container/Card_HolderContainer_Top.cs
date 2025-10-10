using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_HolderContainer_Top : Card_HolderContainer
{
    protected override void OnInitializeCardHolders(int numberOfHolders)
    {
        var maximum = DI.Get<IGrid>().GetMaximumPoint();
        transform.position = new Vector3(-maximum.x, 0, maximum.z + 1.5f);
        var total = Vector3.Distance(transform.position, maximum);
        var segment = total / (numberOfHolders / 2);
        for (int i = 0; i < numberOfHolders/2; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                var holder = CreateHolder(transform);
                holder.transform.localPosition = new Vector3(i * segment, 0, 0);
                holder.transform.localRotation = Quaternion.Euler(0, 0, -30);
                holder.transform.localScale = Vector3.one * CardScaleFactor;
                cardHolders.Add(holder);
            }
        }        
    }
}
