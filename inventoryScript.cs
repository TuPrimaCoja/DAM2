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
    public GameObject prefabAInstanciar;
    public GameObject[] Models = new GameObject[25];
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

        // Para cargar los items
        int cont = -1;
        foreach (var pair in resultDictionary)
        {
            Debug.Log(pair.Key + " = " + pair.Value);
            cont++;
            rawImagesArray[cont].texture = texturesArray[pair.Key];
            textArray[cont].text = "x" + pair.Value;
        }
        
    }
    public void spawnItems()
    {
        GameObject objetoConNombre = GameObject.Find("NombreDelObjeto");
        Instantiate(prefabAInstanciar, objetoConNombre.transform.position, Quaternion.identity);

    }

    public GameObject getGameObjectByID(int id)
    {
        return null;
    }
}


