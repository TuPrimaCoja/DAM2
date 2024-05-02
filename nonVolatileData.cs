using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nonVolatileData : MonoBehaviour
{
    //Esta clase es para guardar variables que no se deben destruir a lo largo del transcurso del programa

    private string userDNI = "";
    private string items = "";
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        Debug.Log("Ahora soy eterno!!!!");
    }


    //Getters y Setters
    public void SetDNI(string theDni)
    {
        userDNI = theDni;
    }

    public string GetDNI()
    {
        return userDNI;
    }

    public void SetItems(string theItems)
    {
        items = theItems;
    }

    public string GetItems()
    {
        return items;
    }
}
