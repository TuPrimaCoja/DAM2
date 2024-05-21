using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Este script es un punto ejemplo, lo que tenemos que hacer es recibir los objetos (si procede) que el 
 * pesonaje tiene desde el servidor, despues, segun el nombre, cargaremos el modelo3D instanciandolo tal
 * como hacemos en el método start.
 * La obtencion de datos y todo el proceso se ha de hacer en el método Start
 */

public class spawnObjectManager : MonoBehaviour
{
    public GameObject rightHand = null;
    public GameObject leftHand = null;
    public GameObject itemRightHand = null;
    public GameObject itemLeftHand = null;
    // Start is called before the first frame update
    void Start()
    {
        //Hemos de tener un diccionario que relaciona el "id" del item con el prefab u objeto 3d, aunque es probable
        //que estén en inventoryScript.
        //recoger items del jugador desde el servidor
        //Si hay items, elegir uno para la mano derecha, mano izquierda ...
        //Instanciar mediante el comando instantiate.

        if (itemRightHand != null && rightHand != null)
        {
            GameObject theGObjectRH = Instantiate(itemRightHand, rightHand.transform.parent);
        }

        //theGObject.transform.Rotate(0, 90, 0);
        if (itemLeftHand != null && leftHand != null)
        {
            GameObject theGObjectLH = Instantiate(itemLeftHand, leftHand.transform.parent);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
