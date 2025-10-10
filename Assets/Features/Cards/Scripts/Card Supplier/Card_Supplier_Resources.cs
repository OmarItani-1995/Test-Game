using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// This was a temp loading solution, it is taking 19Kb and 1811 ms to load all cards 
// from resources, so will switch to a different type 
public class Card_Supplier_Resources : Card_Supplier
{
    public override List<Card> GetCards()
    {
        return Resources.LoadAll<Card>("Cards").ToList();
    }
}
