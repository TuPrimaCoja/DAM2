using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nonVolatileData : MonoBehaviour
{
    private string userDNI = "";
    string userName = "";
    GameObject theGameObject = null;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        Debug.Log("Ahora soy eterno!!!!");
    }

    public void SetDNI(string theDni)
    {
        userDNI = theDni;
    }

    public string GetDNI()
    {
        return userDNI;
    }
}
