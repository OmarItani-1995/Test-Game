using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Holder : MonoBehaviour
{
    [SerializeField] private Card_View cardViewPrefab;

    private Card_View _cardView;
    public void SetCard(Card card)
    {
        if (_cardView == null)
        {
            _cardView = Instantiate(cardViewPrefab, transform);
        }
        
        _cardView.SetCard(card);
    }
}
