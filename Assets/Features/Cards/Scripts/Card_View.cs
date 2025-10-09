using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_View : MonoBehaviour
{
    [SerializeField] private MeshRenderer iconRenderer;

    private Card _card;
    private ICard_MaterialCreator _materialCreator;
    
    void OnEnable()
    {
        _materialCreator = DI.Get<ICard_MaterialCreator>();    
    }
    
    public void SetCard(Card card)
    {
        _card = card;
        iconRenderer.material = _materialCreator.GetMaterial(card);
    }
}
