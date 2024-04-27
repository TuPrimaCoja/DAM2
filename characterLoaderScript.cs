using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class characterLoaderScript : MonoBehaviour
{
    private int globalScale;
    public GameObject prefab; 
    private string urlCargaPersonaje = "http://www.ieslassalinas.org/APP/appCargaPersonaje.php";
    private nonVolatileData fakeSingleton = null;
    public RuntimeAnimatorController characterAnimatorController;
    public Material myMaterial;
    public Texture myTexture0;
    public Texture myTexture1;
    public Texture myTexture2;

    Datos dato;

    private void Start()
    {

        if (fakeSingleton == null)
        {
            GameObject tempGobj = GameObject.Find("variableSaver");
            if (tempGobj != null && tempGobj.GetComponent<nonVolatileData>() != null)
            {
                fakeSingleton = tempGobj.GetComponent<nonVolatileData>();
                Debug.Log("El usuario tiene por DNI: " + fakeSingleton.GetDNI());
            }
        }
        StartCoroutine(conectionMethod(fakeSingleton.GetDNI()));
    }

    //Para con recoger los datos del personaje de cada alumno
    public IEnumerator conectionMethod(string NIE)
    {
        WWWForm form = new WWWForm();
        form.AddField("usuario", NIE);

        using (UnityWebRequest webRequest = UnityWebRequest.Post(urlCargaPersonaje, form))
        {
            yield return webRequest.SendWebRequest();
            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error al realizar la solicitud: " + webRequest.error);
            }
            else
            {
                Debug.Log("Respuesta del servidor: " + webRequest.downloadHandler.text);
                string texto = correctString(webRequest.downloadHandler.text);
                texto = texto.Replace("\\", "");
                dato = JsonUtility.FromJson<Datos>(texto);
                Debug.Log("scale: " + dato.scale + ", hairColor: " + dato.hairColor + ", skin: " + dato.skin + ", race: " + dato.race + ", sex: " + dato.sex);
                globalScale = dato.scale;
                loadCharacter();
            }
        }
    }

    //Para corregir el JSON
    public string correctString(string cadena)
    {
        string caracteresPermitidos = "{}\"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789:,";
        string cadenaModificada = "";
        foreach (char c in cadena)
        {
            if (caracteresPermitidos.Contains(c.ToString()))
            {
                cadenaModificada += c;
            }
        }
        return cadenaModificada;
    }

    //Para cargar de los prefabs
    public void loadCharacter()
    {
        //personaje
        GameObject instantiatedPrefab = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        Vector3 scale = new Vector3(globalScale, globalScale, globalScale);
        instantiatedPrefab.transform.localScale = scale;

        //skin
        if (dato.skin == 0)
        {
            myMaterial.mainTexture = myTexture0;
            Debug.Log("Se colocó la textura 0");
        }
        else if(dato.skin == 1)
        {
            myMaterial.mainTexture = myTexture1;
            Debug.Log("Se colocó la textura 1");
        }
        else
        {
            myMaterial.mainTexture = myTexture2;
            Debug.Log("Se colocó la textura 2");
        }
        instantiatedPrefab.AddComponent<MeshRenderer>().material = myMaterial;

        //Animation
        Animator animator = instantiatedPrefab.GetComponent<Animator>();
        if (animator != null && characterAnimatorController != null)
        {
            animator.runtimeAnimatorController = characterAnimatorController;
        }
        else
        {
            Debug.LogError("Animator o AnimatorController no encontrados.");
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
    }
}