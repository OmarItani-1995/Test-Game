using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_MaterialCreator : MonoBehaviour, ICard_MaterialCreator
{
    public Material mainMaterial;
    private Dictionary<Card, Material> materials = new Dictionary<Card, Material>();

    void Awake()
    {
        DI.Register<ICard_MaterialCreator>(this);
    }
    
    public Material GetMaterial(Card card)
    {
        if (materials.ContainsKey(card))
        {
            return materials[card];
        }
        else
        {
            Material newMaterial = new Material(mainMaterial);
            newMaterial.SetTexture("_BaseMap", card.icon);
            materials.Add(card, newMaterial);
            return newMaterial;
        }
    }
}

public interface ICard_MaterialCreator
{
    Material GetMaterial(Card card);
}