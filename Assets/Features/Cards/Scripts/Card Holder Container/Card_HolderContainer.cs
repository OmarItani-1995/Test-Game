using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Card_HolderContainer : MonoBehaviour
{
    [SerializeField] private GameObject cardHolderPrefab;
    [SerializeField] [Range(0, 1)] private float betweenTransitionDelay;
    protected List<Card_Holder> cardHolders = new List<Card_Holder>();
    
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

    public IEnumerator TransitionCards(Card_HolderContainer container)
    {
        yield return new WaitForSeconds(0.5f);
        var holders = CopyHolders();
        var otherHolders = container.CopyHolders();
        for (int i = 0; i < holders.Count; i++)
        {
            var holder = holders[i];
            var otherHolder = otherHolders.GetRandomAndRemove();
            holder.TransitionCard(otherHolder);
            yield return new WaitForSeconds(betweenTransitionDelay);
        }
    }

    public void TransitionCards(List<Card_Holder> oldHolders)
    {
        foreach (var oldHolder in oldHolders)
        {
            var newHolder = GetFirstEmptyHolder();
            if (newHolder == null)
            {
                Debug.LogError("No empty holder found");
                return;
            }
            oldHolder.TransitionCard(newHolder);
        }
    }

    private Card_Holder GetFirstEmptyHolder()
    {
        for (int i = 0; i < cardHolders.Count; i++)
        {
            if (cardHolders[i].IsEmpty())
            {
                return cardHolders[i];
            }
        }

        return null;
    }

    private List<Card_Holder> CopyHolders()
    {
        return cardHolders.Copy();
    }

    public void HideAllCards()
    {
        foreach (var card in cardHolders)
        {
            card.HideCard();
        }
    }

    public void EnableInput()
    {
        foreach (var holder in cardHolders)
        {
            holder.EnableInput();
        }
    }
}


