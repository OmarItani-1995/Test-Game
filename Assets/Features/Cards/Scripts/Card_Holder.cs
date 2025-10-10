using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Holder : MonoBehaviour
{
    [SerializeField] private GameObject cardViewPrefab;

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
            _cardView = Instantiate(cardViewPrefab).GetComponent<Card_View>();
            _cardView.transform.SetParent(transform);
            _cardView.transform.localPosition = Vector3.zero;
            _cardView.transform.localScale = Vector3.one;
        }
        
        _cardView.SetCard(card);
    }

    public void TransitionCard(Card_Holder otherHolder)
    {
        otherHolder.SetCardView(_cardView);
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
        SetCardView(secondHolder._cardView);
        secondHolder.SetCardView(tempView);
    }
    
    private void SetCardView(Card_View cardView)
    {
        _cardView = cardView;
        _cardView.transform.SetParent(transform);
    }
    
    private void PlayFlipAudio()
    {
        _audioManager.PlayAudio(Audio_ClipType.Card_Flip, 0.5f);
    }
}
