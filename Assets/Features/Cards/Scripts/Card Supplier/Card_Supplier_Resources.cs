using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Card_Supplier_Resources : Card_Supplier
{
    public override List<Card> GetCards()
    {
        return Resources.LoadAll<Card>("Cards").ToList();
    }
}
