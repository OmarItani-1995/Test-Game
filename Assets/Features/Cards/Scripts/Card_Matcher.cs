using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Matcher : MonoBehaviour, ICardMatcher
{
    private ICardManager _cardManager;
    
    private List<Card_Holder> _shownCards = new List<Card_Holder>();
    private bool isTakingInput = false;
    
    void Awake()
    {
        DI.Register<ICardMatcher>(this);
    }

    void Start()
    {
        _cardManager = DI.Get<ICardManager>();
        Msg.RegisterListener(typeof(Msg_GameStarted), OnGameStarted);
    }

    private void OnGameStarted(Message message)
    {
        isTakingInput = true;
    }

    public void CardClicked(Card_Holder holder)
    {
        if (!isTakingInput) return;

        if (holder.IsHidden())
        {
            ShowCard(holder);
        }
        else
        {
            HideCard(holder);
        }
    }

    private void ShowCard(Card_Holder holder)
    {
        if (_shownCards.Contains(holder)) return;
        
        if (_shownCards.Count >= 2) return;

        _shownCards.Add(holder);
        holder.ShowCard();

        if (_shownCards.Count == 2)
        {
            CheckMatch();
        }   
    }

    private void CheckMatch()
    {
        isTakingInput = false;
        if (_shownCards[0].HasSameCard(_shownCards[1]))
        {
            Invoke("MatchHappened", 1f);
        }
        else
        {
            Invoke("ResetCards", 1f);
        }
    }

    private void HideCard(Card_Holder holder)
    {
        if (!_shownCards.Contains(holder))
        {
            return;
        }
        
        holder.HideCard();
        _shownCards.Remove(holder);
    }

    private void MatchHappened()
    {
        _cardManager.MatchHappened(_shownCards);
        _shownCards.Clear();
        isTakingInput = true;
    }

    private void ResetCards()
    {
        foreach (var card in _shownCards)
        {
            card.HideCard();
        }
        _shownCards.Clear();
        isTakingInput = true;
    }
}

public interface ICardMatcher
{
    void CardClicked(Card_Holder holder);
}