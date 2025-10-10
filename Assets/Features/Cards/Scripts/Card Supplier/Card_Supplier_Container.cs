using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Card_Supplier_Container : Card_Supplier
{
    public List<Card> cards;
    
    public override List<Card> GetCards()
    {
        return cards;
    }
    
    #if UNITY_EDITOR
    [ContextMenu("Load Cards")]
    private void Co_LoadCards()
    {
        cards = AssetUtility.LoadAllAssetsOfType<Card>();
        EditorUtility.SetDirty(this);
    }
    #endif
}
