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
                Datos dato = JsonUtility.FromJson<Datos>(texto);
                Debug.Log("Escala: " + dato.escala + ", ColorHex: " + dato.colorHex);
                globalScale = dato.escala;

                // Una vez obtenida la escala, llamamos al método de instanciación
                transformMethod();
            }
        }
    }

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

    public void transformMethod()
    {
        // Crea una instancia del prefab
        GameObject instantiatedPrefab = Instantiate(prefab, Vector3.zero, Quaternion.identity);

        // Escala el objeto instanciado
        Vector3 scale = new Vector3(globalScale , globalScale , globalScale );
        instantiatedPrefab.transform.localScale = scale;

        // Asigna el AnimatorController al prefab
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

    [System.Serializable]
    public class Datos
    {
        public int escala;
        public string colorHex;
    }
}