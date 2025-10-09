using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Holder : MonoBehaviour
{
    [SerializeField] private Card_View cardViewPrefab;
    
    private ICardMatcher _cardMatcher;
    private Card_View _cardView;
    private bool _isHidden = false;
    private bool _canTakeInput = false;
    
    void OnEnable()
    {
        _cardMatcher = DI.Get<ICardMatcher>();
    }
    public void SetCard(Card card)
    {
        if (_cardView == null)
        {
            _cardView = Instantiate(cardViewPrefab, transform);
        }
        
        _cardView.SetCard(card);
    }

    public void TransitionCard(Card_Holder otherHolder)
    {
        otherHolder.SetCardView(_cardView);
        _cardView = null;
    }

    private void SetCardView(Card_View cardView)
    {
        _cardView = cardView;
        _cardView.transform.SetParent(transform);
    }

    public void HideCard()
    {
        if (_cardView != null)
        {
            _isHidden = true;
            _cardView.HideCard();
        }
    }

    public void ShowCard()
    {
        if (_cardView != null)
        {
            _isHidden = false;
            _cardView.ShowCard();
        }
    }

    private void OnMouseDown()
    {
        if (_canTakeInput && GetCard() != null)
        {
            _cardMatcher.CardClicked(this);
        }
    }

    public Card GetCard()
    {
        return _cardView != null ? _cardView.GetCard() : null;
    }

    public bool HasSameCard(Card_Holder shownCard)
    {
        var card = GetCard();
        return card != null && card.Equals(shownCard.GetCard());
    }

    public bool IsHidden()
    {
        return _isHidden;
    }

    public bool IsEmpty()
    {
        return _cardView == null;
    }

    public void EnableInput()
    {
        _canTakeInput = true;
    }
}
