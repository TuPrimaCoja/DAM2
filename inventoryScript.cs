using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class inventoryScript : MonoBehaviour
{
    public RawImage[] rawImagesArray = new RawImage[25];
    public TMP_Text[] textArray = new TMP_Text[25];
    public Texture[] texturesArray = new Texture[30];
    public string itemsFromCharacterLoader;
    public void SetItems(string items)
    {
        itemsFromCharacterLoader = items;
        loadItems(itemsFromCharacterLoader);
    }

    public void loadItems(string items)
    {
        Debug.Log("Items desde el script de inventario : " + items);

        // Para separar los caracteres
        string[] parts = items.Split(';');
        Dictionary<int, int> resultDictionary = new Dictionary<int, int>();
        foreach (string part in parts)
        {
            string[] subParts = part.Split('-');
            int key, value;
            if (int.TryParse(subParts[0], out key) && int.TryParse(subParts[1], out value))
            {
                resultDictionary.Add(key, value);
            }
        }

        foreach (KeyValuePair<int, int> entry in resultDictionary)
        {
            int imageIndex = entry.Key;
            int textureIndex = entry.Value;

            if (imageIndex >= 0 && imageIndex < rawImagesArray.Length && textureIndex >= 0 && textureIndex < texturesArray.Length)
            {
                rawImagesArray[imageIndex].texture = texturesArray[textureIndex];
            }
        }
    }
}


