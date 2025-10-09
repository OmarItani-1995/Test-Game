using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Manager : MonoBehaviour, ICardManager
{
    void Awake()
    {
        DI.Register<ICardManager>(this);
    }

    [SerializeField] private Card_HolderContainer _gridContainer;
    [SerializeField] private Card_HolderContainer _sideContainer;
    private Card_Supplier _cardSupplier;
    
    void Start()
    {
        _cardSupplier = GetComponent<Card_Supplier>();
        if (_cardSupplier == null)
        {
            Debug.LogError("Card_Manager requires a Card_Supplier component, add the component in the same gameobject");
        }
    }

    public IEnumerator SetUpCards(int count)
    {
        _sideContainer.InitializeCardHolders(count);
        _gridContainer.InitializeCardHolders(count);
        
        List<Card> cards = GetCards(count / 2).DuplicateContent();
        _sideContainer.SetCards(cards);
        yield return null;
    }

    private List<Card> GetCards(int count)
    {
        var allCards = _cardSupplier.GetCards();
        var cards = new List<Card>();
        for (int i = 0; i < count; i++)
        {
            cards.Add(allCards.GetRandomAndRemove());
        }
        return cards;
    }
}

public interface ICardManager
{
    IEnumerator SetUpCards(int count);
}