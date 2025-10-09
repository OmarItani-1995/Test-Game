using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_MaterialCreator : MonoBehaviour
{
    public static Card_MaterialCreator instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    
    public Material mainMaterial;
    private Dictionary<Card, Material> materials = new Dictionary<Card, Material>();

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
