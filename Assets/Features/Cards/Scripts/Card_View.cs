using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_View : MonoBehaviour
{
    public MeshRenderer iconRenderer;

    private Card _card;

    public void SetCard(Card card)
    {
        _card = card;
        iconRenderer.material = Card_MaterialCreator.instance.GetMaterial(card);
    }
}
