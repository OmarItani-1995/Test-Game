using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card_HolderContainer : MonoBehaviour
{
    public GameObject cardHolderPrefab;
    public List<Card_Holder> cardHolders = new List<Card_Holder>();
    
    public abstract void InitializeCardHolders(int numberOfHolders);
    public void SetCards(List<Card> cards)
    {
        for (int i = 0; i < cardHolders.Count; i++)
        {
            cardHolders[i].SetCard(cards[i]);
        }
    }
    
    protected Card_Holder CreateHolder(Transform parent)
    {
        var holderObj = Instantiate(cardHolderPrefab, parent);
        var holder = holderObj.GetComponent<Card_Holder>();
        return holder;
    }
}


