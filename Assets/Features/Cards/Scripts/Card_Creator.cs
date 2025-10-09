using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// This script is used to create Card ScriptableObjects from a list of icons.
/// You can add the prefab in the scene, assign the icons and then press create cards.
/// </summary>
public class Card_Creator : MonoBehaviour
{
    public List<Texture2D> icons;
}

#if UNITY_EDITOR
[CustomEditor(typeof(Card_Creator))]
public class Card_Creator_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Card_Creator myScript = (Card_Creator)target;
        if (GUILayout.Button("Create Cards"))
        {
            CreateCards(myScript.icons);
        }
    }

    private void CreateCards(List<Texture2D> icons)
    {
        string path = "Assets/Features/Cards/Resources/Cards/";
        
        if (!AssetDatabase.IsValidFolder(path))
        {
            AssetDatabase.CreateFolder("Assets/Features/Cards/Resources", "Cards");
        }
        
        foreach (Texture2D icon in icons)
        {
            Card newCard = ScriptableObject.CreateInstance<Card>();
            newCard.icon = icon;

            string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + icon.name + ".asset");

            AssetDatabase.CreateAsset(newCard, assetPathAndName);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
#endif
