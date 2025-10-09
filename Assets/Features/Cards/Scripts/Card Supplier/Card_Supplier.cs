using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// i use a card supplier base in case we want to change the way we supply cards in the future.
/// Didnt use di interface because this is an internal class to the Card System
/// </summary>
public abstract class Card_Supplier : MonoBehaviour
{
    public abstract List<Card> GetCards();
}
