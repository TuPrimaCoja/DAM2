using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hideUIScript : MonoBehaviour
{
    public GameObject uiContainer1;
    public GameObject uiContainer2;
    public GameObject inventory;


    private void Start()
    {
        inventory.SetActive(false);
        uiContainer1.SetActive(true);
        uiContainer2.SetActive(false);
    }
    public void hideUI()
    {
        if (uiContainer1.activeSelf)
        {
            uiContainer1.SetActive(false);
            uiContainer2.SetActive(true);
        }

        else if (uiContainer2.activeSelf)
        {
            uiContainer2.SetActive(false);
            uiContainer1.SetActive(true);
        }
    }

    public void loadInventory()
    {
        inventory.SetActive(true);
    }

    public void removeInventory()
    {
        inventory.SetActive(false);
    }
}

