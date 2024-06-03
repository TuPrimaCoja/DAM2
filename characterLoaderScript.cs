using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Text.RegularExpressions;
using TMPro;
using System;

public class characterLoaderScript : MonoBehaviour
{

    private string loadCharacterURL = "http://www.ieslassalinas.org/APP/appCargaPersonaje.php";
    //private nonVolatileData fakeSingleton = null;
    public TMP_Text nameTextField;
    public Canvas nonCharacterCanvas;

    //razas

    public GameObject maleHumanCharacter;
    public GameObject femaleHumanCharacter;

    public GameObject maleElfeCharacter;
    public GameObject femaleElfeCharacter;

    public GameObject maleDwarfCharacter;
    public GameObject femaleDwarfCharacter;

    //Materiales y texturas

        //humanos    

    public Material maleHumanMaterial;
    public Material femaleHumanMaterial;

    public Texture femaleHumanTexture0;
    public Texture femaleHumanTexture1;
    public Texture femaleHumanTexture2;

    public Texture maleHumanTexture0;
    public Texture maleHumanTexture1;
    public Texture maleHumanTexture2;

        //elfos    

    public Material maleElfeMaterial;
    public Material femaleElfeMaterial;

    public Texture femaleElfeTexture0;
    public Texture femaleElfeTexture1;
    public Texture femaleElfeTexture2;

    public Texture maleElfeTexture0;
    public Texture maleElfeTexture1;
    public Texture maleElfeTexture2;

        //enanos   

    public Material maleDwarfMaterial;
    public Material femaleDwarfMaterial;

    public Texture femaleDwarfTexture0;
    public Texture femaleDwarfTexture1;
    public Texture femaleDwarfTexture2;

    public Texture maleDwarfTexture0;
    public Texture maleDwarfTexture1;
    public Texture maleDwarfTexture2;

    //pelo

    public Material hairMaterial;
    public GameObject[] beardHairPrefab;
    public GameObject[] browsHairPrefab;
    public GameObject[] mainHairPrefab;

    //oficio

    public GameObject[] knightClothes;
    public GameObject[] wizardClothes;
    public GameObject[] exploratorClothes;

    //

    private Datos date;
    private inventoryScript iS;

    private void Start()
    {
        nonCharacterCanvas.gameObject.SetActive(false);
        
        iS = GetComponent<inventoryScript>();
        if (iS == null)
        {
            Debug.LogError("error con el objeto iS");
        }

        /*
        if (fakeSingleton == null) 
        {
            Debug.Log("Sigo quedandome aqui");
            GameObject tempGobj = GameObject.Find("variableSaver");
            if (tempGobj != null && tempGobj.GetComponent<nonVolatileData>() != null)
            {
                fakeSingleton = tempGobj.GetComponent<nonVolatileData>();
                Debug.Log("El usuario tiene por DNI: " + nonVolatileData.GetDNI());
            }
        }
        if (fakeSingleton != null)
        {
            StartCoroutine(conectionMethod(nonVolatileData.GetDNI()));
        }
        */
        Debug.Log("El usuario tiene por DNI: " + nonVolatileData.GetDNI());
        if (nonVolatileData.GetDNI() != "")
        {
            StartCoroutine(conectionMethod(nonVolatileData.GetDNI()));
        }
    }

    //Para con recoger los datos del personaje de cada alumno
    public IEnumerator conectionMethod(string NIE)
    {
        Debug.Log("[WebRequest Call with value " + NIE + " ]");
        date = null;
        WWWForm form = new WWWForm();
        form.AddField("usuario", NIE);
        UnityWebRequest webRequest = null;
        using (webRequest = UnityWebRequest.Post(loadCharacterURL, form))
        {
            yield return webRequest.SendWebRequest();
            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error al realizar la solicitud: " + webRequest.error);
            }
            else
            {
                Debug.Log("Respuesta del servidor: " + webRequest.downloadHandler.text);
                string texto = webRequest.downloadHandler.text;
                texto = Regex.Replace(texto, @"[^a-zA-Z0-9:\-{},;"" ]", "");
                try
                {
                    date = JsonUtility.FromJson<Datos>(texto);
                    if (date != null)
                    {
                        Debug.Log("scale: " + date.scale + ", hairColor: " + date.hairColor + ", skin: " + date.skin + ", race: " + date.race + ", sex: " + date.sex + ", items: " + date.items + ", hairType: " + date.hairType + ", profesion: " + date.profesion + ", nombre: " + date.name);
                        iS.SetItems(date.items);
                        nameTextField.text = "Nombre : " + date.name;
                        loadCharacter();
                    }
                    date = JsonUtility.FromJson<Datos>(texto);
                    if (date != null)
                    {
                        Debug.Log("scale: " + date.scale + ", hairColor: " + date.hairColor + ", skin: " + date.skin + ", race: " + date.race + ", sex: " + date.sex + ", items: " + date.items + ", hairType: " + date.hairType + ", profesion: " + date.profesion + ", nombre: " + date.name);
                        iS.SetItems(date.items);
                        nameTextField.text = "Nombre : " + date.name;
                        loadCharacter();
                    }
                }
                catch (Exception e)
                {
                    Debug.Log("[conectionMethod] El json que ha llegado NO es válido, contenido: "+ texto + " error: "+e.ToString());
                    nonCharacterLoaded();
                }
                
            }
        }
    }
    public void loadCharacter()
    {
    //PARA CARGAR EL CUERPO

        GameObject usedPrefab;
        Material usedMaterial;

        Debug.Log("Cargando avatar . . .");
            //humanos
        
        if (date.race == 0)
        {
            Debug.Log("Se colocó el prefab humano");
            //sexo
            if (date.sex == 0)
            {
                Debug.Log("Se colocó el sexo hombre");
                //piel
                usedPrefab = maleHumanCharacter;
                usedMaterial = maleHumanMaterial;
                if (date.skin == 0)
                {
                    usedMaterial.mainTexture = maleHumanTexture0;
                    Debug.Log("Se colocó la textura 0");
                    instantiateBody(usedPrefab, usedMaterial);
                }
                else if (date.skin == 1)
                {
                    usedMaterial.mainTexture = maleHumanTexture1;
                    Debug.Log("Se colocó la textura 1");
                    instantiateBody(usedPrefab, usedMaterial);
                }
                else
                {
                    usedMaterial.mainTexture = maleHumanTexture2;
                    Debug.Log("Se colocó la textura 2");
                    instantiateBody(usedPrefab, usedMaterial);
                }
            }
            else
            {
                Debug.Log("Se colocó el sexo mujer");
                //piel
                usedPrefab = femaleHumanCharacter;
                usedMaterial = femaleHumanMaterial;
                if (date.skin == 0)
                {
                    usedMaterial.mainTexture = femaleHumanTexture0;
                    Debug.Log("Se colocó la textura 0");
                    instantiateBody(usedPrefab, usedMaterial);
                }
                else if (date.skin == 1)
                {
                    usedMaterial.mainTexture = femaleHumanTexture1;
                    Debug.Log("Se colocó la textura 1");
                    instantiateBody(usedPrefab, usedMaterial);
                }
                else
                {
                    usedMaterial.mainTexture = femaleHumanTexture2;
                    Debug.Log("Se colocó la textura 2");
                    instantiateBody(usedPrefab, usedMaterial);
                }
            }
        }

            //elfos

        if (date.race == 1)
        {
            Debug.Log("Se colocó el prefab elfo");
            //sexo
            if (date.sex == 0)
            {
                Debug.Log("Se colocó el sexo hombre");
                //piel
                usedPrefab = maleElfeCharacter;
                usedMaterial = maleElfeMaterial;
                if (date.skin == 0)
                {
                    usedMaterial.mainTexture = maleElfeTexture0;
                    Debug.Log("Se colocó la textura 0");
                    instantiateBody(usedPrefab, usedMaterial);
                }
                else if (date.skin == 1)
                {
                    usedMaterial.mainTexture = maleElfeTexture1;
                    Debug.Log("Se colocó la textura 1");
                    instantiateBody(usedPrefab, usedMaterial);
                }
                else
                {
                    usedMaterial.mainTexture = maleElfeTexture2;
                    Debug.Log("Se colocó la textura 2");
                    instantiateBody(usedPrefab, usedMaterial);
                }
            }
            else
            {
                Debug.Log("Se colocó el sexo mujer");
                //piel
                usedPrefab = femaleElfeCharacter;
                usedMaterial = femaleElfeMaterial;
                if (date.skin == 0)
                {
                    usedMaterial.mainTexture = femaleElfeTexture0;
                    Debug.Log("Se colocó la textura 0");
                    instantiateBody(usedPrefab, usedMaterial);
                }
                else if (date.skin == 1)
                {
                    usedMaterial.mainTexture = femaleElfeTexture1;
                    Debug.Log("Se colocó la textura 1");
                    instantiateBody(usedPrefab, usedMaterial);
                }
                else
                {
                    usedMaterial.mainTexture = femaleElfeTexture2;
                    Debug.Log("Se colocó la textura 2");
                    instantiateBody(usedPrefab, usedMaterial);
                }
            }
        }

            //enanos

        if (date.race == 2)
        {
            Debug.Log("Se colocó el prefab enano");
            //sexo
            if (date.sex == 0)
            {
                Debug.Log("Se colocó el sexo hombre");
                //piel
                usedPrefab = maleDwarfCharacter;
                usedMaterial = maleDwarfMaterial;
                if (date.skin == 0)
                {
                    usedMaterial.mainTexture = maleDwarfTexture0;
                    Debug.Log("Se colocó la textura 0");
                    instantiateBody(usedPrefab, usedMaterial);
                    
                }
                else if (date.skin == 1)
                {
                    usedMaterial.mainTexture = maleDwarfTexture1;
                    Debug.Log("Se colocó la textura 1");
                    instantiateBody(usedPrefab, usedMaterial);
                }
                else
                {
                    usedMaterial.mainTexture = maleDwarfTexture2;
                    Debug.Log("Se colocó la textura 2");
                    instantiateBody(usedPrefab, usedMaterial);
                }
            }
            else
            {
                Debug.Log("Se colocó el sexo mujer");
                //piel
                usedPrefab = femaleDwarfCharacter;
                usedMaterial = femaleDwarfMaterial;
                if (date.skin == 0)
                {
                    usedMaterial.mainTexture = femaleDwarfTexture0;
                    Debug.Log("Se colocó la textura 0");
                    instantiateBody(usedPrefab, usedMaterial);
                }
                else if (date.skin == 1)
                {
                    usedMaterial.mainTexture = femaleDwarfTexture1;
                    Debug.Log("Se colocó la textura 1");
                    instantiateBody(usedPrefab, usedMaterial);
                }
                else
                {
                    usedMaterial.mainTexture = femaleDwarfTexture2;
                    Debug.Log("Se colocó la textura 2");
                    instantiateBody(usedPrefab, usedMaterial);
                }
            }
        }

    //PARA CARGAR EL PELO

        string[] numeros = date.hairType.Split(':');
        int num1 = int.Parse(numeros[0]); //barba
        int num2 = int.Parse(numeros[1]); //cejas
        int num3 = int.Parse(numeros[2]); //pelo principal

        Debug.Log("Cargando pelo . . .");
        // para la barba

            // pelo para hombre
        if (num1 == 1)
        {
            instantiateHair(beardHairPrefab[0]);
            Debug.Log("Barba 1 cargada");
        }
        else if (num1 == 2)
        {
            instantiateHair(beardHairPrefab[1]);
            Debug.Log("Barba 2 cargada");
        }
        else if(num1 == 3)
        {
            instantiateHair(beardHairPrefab[2]);
            Debug.Log("Barba 3 cargada");
        }
            //sin pelo
        else if (num1 == 0)
        {
            Debug.Log("No hay ninguna barba que cargar");
        }

        // para las cejas

            //pelo para hombre
        if (num2 == 1)
        {
            instantiateHair(browsHairPrefab[0]);
            Debug.Log("Cejas de hombre 1 cargadas");
        }
        else if (num2 == 2)
        {
            instantiateHair(browsHairPrefab[1]);
            Debug.Log("Cejas de hombre 2 cargadas");
        }
            //pelo para mujer
        else if (num2 == 3)
        {
            instantiateHair(browsHairPrefab[2]);
            Debug.Log("Cejas de mujer 1 cargadas");
        }
        else if (num2 == 4)
        {
            instantiateHair(browsHairPrefab[3]);
            Debug.Log("Cejas de mujer 2 cargadas");
        }
            //sin pelo
        else if (num2 == 0)
        {
            Debug.Log("No hay ningunas cejas que cargar");
        }

        // para el pelo principal

            // pelo para hombre 
        if (num3 == 1)
        {
            instantiateHair(mainHairPrefab[0]);
            Debug.Log("Pelo principal de hombre 1 cargado");
        }
        else if (num3 == 2)
        {
            instantiateHair(mainHairPrefab[1]);
            Debug.Log("Pelo principal de hombre 2 cargado");
        }
        else if (num3 == 3)
        {
            instantiateHair(mainHairPrefab[2]);
            Debug.Log("Pelo principal de hombre 3 cargado");
        }
            //pelo para mujer
        else if (num3 == 4)
        {
            instantiateHair(mainHairPrefab[3]);
            Debug.Log("Pelo principal de mujer 1 cargado");
        }
        else if (num3 == 5)
        {
            instantiateHair(mainHairPrefab[4]);
            Debug.Log("Pelo principal de mujer 2 cargado");
        }
        else if (num3 == 6)
        {
            instantiateHair(mainHairPrefab[5]);
            Debug.Log("Pelo principal de mujer 3 cargado");
        }
            //sin pelo
        else if (num3 == 0)
        {
            Debug.Log("No hay ningun pelo principal que cargar");
        }
        
    // PARA CARGAR LAS ROPAS SEGUN EL OFICIO

        //ropa de hombre
        if (date.sex == 0)
        {
            if (date.profesion == 1)
            {
                    instantiateOthers(knightClothes[0]);
                    Debug.Log("Se instancio la ropa de oficio de caballero");
            }
            else if (date.profesion == 2)
            {
                    instantiateOthers(wizardClothes[0]);
                    Debug.Log("Se instancio la ropa de oficio de mago");
            }
            else if (date.profesion == 3)
            {
                    instantiateOthers(exploratorClothes[0]);
                    Debug.Log("Se instancio la ropa de oficio de explorador");
            }
        }

                //ropa de mujer
        else
        {
            if (date.profesion == 1)
            {
                    instantiateOthers(knightClothes[1]);
                    Debug.Log("Se instancio la ropa de oficio de caballera");
            }
            else if (date.profesion == 2)
            {
                    instantiateOthers(wizardClothes[1]);
                    Debug.Log("Se instancio la ropa de oficio de maga");
            }
            else if (date.profesion == 3)
            {
                    instantiateOthers(exploratorClothes[1]);
                    Debug.Log("Se instancio la ropa de oficio de exploradora");
            }
        }
    }

    private void nonCharacterLoaded()
    {
        nonCharacterCanvas.gameObject.SetActive(true);
    }
    private void instantiateBody(GameObject prefab, Material usedMaterial)
    {
        GameObject usedPrefab;
        usedPrefab = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        Vector3 scale = new Vector3(date.scale, date.scale, date.scale);
        usedPrefab.transform.localScale = scale;
        usedPrefab.AddComponent<MeshRenderer>().material = usedMaterial;
    }

    private void instantiateOthers(GameObject prefab)
    {
        GameObject usedPrefab;
        usedPrefab = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        Vector3 scale = new Vector3(date.scale, date.scale, date.scale);
        usedPrefab.transform.localScale = scale;
    }
    private void instantiateHair(GameObject prefab)
    {
        GameObject usedPrefab;
        usedPrefab = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        Vector3 scale = new Vector3(date.scale, date.scale, date.scale);
        usedPrefab.transform.localScale = scale;

        date.hairColor = date.hairColor.Replace("#", "");
        Color color = HexToColor(date.hairColor);
        hairMaterial.color = color;
        Debug.Log("Color cambiado a: " + color);
        
    }

    //para convertir de valores hexadecimales a rgb
    private Color HexToColor(string hex)
    {
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        return new Color32(r, g, b, 255);
    }
}
    
    
//Para dar formato a la estructura del JSON

[System.Serializable]
public class Datos
{
    public int scale;
    public string hairColor;
    public int skin;
    public int race;
    public int sex;
    public string items;
    public string hairType;
    public int profesion;
    public string name;
}
