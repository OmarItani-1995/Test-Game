using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Holder : MonoBehaviour
{
    [SerializeField] private Card_View cardViewPrefab;

    private IAudioManager _audioManager;
    private ICardMatcher _cardMatcher;
        
    private Card_View _cardView;
    private bool _isHidden = false;
    private bool _canTakeInput = false;
    
    void OnEnable()
    {
        _cardMatcher = DI.Get<ICardMatcher>();
        _audioManager = DI.Get<IAudioManager>();
    }
    
    private void OnMouseDown()
    {
        if (_canTakeInput && GetCard() != null)
        {
            _cardMatcher.CardClicked(this);
        }
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
        otherHolder.SetCardView(_cardView, true);
        _cardView = null;
    }

    public void HideCard(bool playAudio = false)
    {
        if (_cardView != null)
        {
            _isHidden = true;
            _cardView.HideCard();
            if (playAudio)
            {
                PlayFlipAudio();
            }
        }
    }

    public void ShowCard()
    {
        if (_cardView != null)
        {
            _isHidden = false;
            _cardView.ShowCard();
            PlayFlipAudio();
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
    public void DisableInput()
    {
        _canTakeInput = false;
    }
    
    public void SwitchViews(Card_Holder secondHolder)
    {
        var tempView = _cardView;
        SetCardView(secondHolder._cardView, false);
        secondHolder.SetCardView(tempView, false);
    }
    
    private void SetCardView(Card_View cardView, bool playAudio)
    {
        _cardView = cardView;
        _cardView.transform.SetParent(transform);
        if (playAudio)
            PlayTransitionAudio();
    }
    
    private void PlayFlipAudio()
    {
        _audioManager.PlayAudio(Audio_ClipType.Card_Flip, 0.5f);
    }

    private void PlayTransitionAudio()
    {
        _audioManager.PlayAudio(Audio_ClipType.Deal_Card, 0.5f);
    }
}
