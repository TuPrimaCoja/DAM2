using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Text.RegularExpressions;

public class characterLoaderScript : MonoBehaviour
{


    public GameObject maleCharacter;
    public GameObject femaleCharacter;

    private string loadCharacterURL = "http://www.ieslassalinas.org/APP/appCargaPersonaje.php";
    private nonVolatileData fakeSingleton = null;

    public RuntimeAnimatorController maleCharacterAnimatorController;
    public RuntimeAnimatorController femaleCharacterAnimatorController;
    private RuntimeAnimatorController usedCharacterAnimatorController;

    public Material maleMaterial;
    public Material femaleMaterial;
    private Material usedMaterial;

    public Texture femaleTexture0;
    public Texture femaleTexture1;
    public Texture femaleTexture2;

    public Texture maleTexture0;
    public Texture maleTexture1;
    public Texture maleTexture2;

    private Texture usedTexture0;
    private Texture usedTexture1;
    private Texture usedTexture2;

    private Datos date;
    private inventoryScript iS;

    private void Start()
    {
        iS = GetComponent<inventoryScript>();
        if (iS == null)
        {
            Debug.LogError("error con el objeto iS");
            return;
        }

        if (fakeSingleton == null)
        {
            GameObject tempGobj = GameObject.Find("variableSaver");
            if (tempGobj != null && tempGobj.GetComponent<nonVolatileData>() != null)
            {
                fakeSingleton = tempGobj.GetComponent<nonVolatileData>();
                Debug.Log("El usuario tiene por DNI: " + fakeSingleton.GetDNI());
            }
        }
        if (fakeSingleton != null)
        {
            StartCoroutine(conectionMethod(fakeSingleton.GetDNI()));
        }
    }

    //Para con recoger los datos del personaje de cada alumno
    public IEnumerator conectionMethod(string NIE)
    {
        WWWForm form = new WWWForm();
        form.AddField("usuario", NIE);
        using (UnityWebRequest webRequest = UnityWebRequest.Post(loadCharacterURL, form))
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
                date = JsonUtility.FromJson<Datos>(texto);
                //falta por poner : colorPelo, peso
                Debug.Log("scale: " + date.scale + ", hairColor: " + date.hairColor + ", skin: " + date.skin + ", race: " + date.race + ", sex: " + date.sex + ", items: " + date.items);
                iS.SetItems(date.items);
                loadCharacter();
            }
        }
    }

    //Para cargar de los prefabs
    public void loadCharacter()
    {
        GameObject instantiatedPrefab;

        //sex
        if (date.sex == 1)
        {
            instantiatedPrefab = Instantiate(maleCharacter, Vector3.zero, Quaternion.identity);
            usedCharacterAnimatorController = maleCharacterAnimatorController;
            usedMaterial = maleMaterial;
            usedTexture0 = maleTexture0;
            usedTexture1 = maleTexture1;
            usedTexture2 = maleTexture2;
        }
        else
        {
            instantiatedPrefab = Instantiate(femaleCharacter, Vector3.zero, Quaternion.identity);
            usedCharacterAnimatorController = femaleCharacterAnimatorController;
            usedMaterial = femaleMaterial;
            usedTexture0 = femaleTexture0;
            usedTexture1 = femaleTexture1;
            usedTexture2 = femaleTexture2;
        }

        instantiatedPrefab.GetComponent<Animator>().runtimeAnimatorController = usedCharacterAnimatorController;
        Vector3 scale = new Vector3(date.scale, date.scale, date.scale);
        instantiatedPrefab.transform.localScale = scale;

        //skin
        if (date.skin == 0)
        {
            usedMaterial.mainTexture = usedTexture0;
            Debug.Log("Se colocó la textura 0");
        }
        else if (date.skin == 1)
        {
            usedMaterial.mainTexture = usedTexture1;
            Debug.Log("Se colocó la textura 1");
        }
        else
        {
            usedMaterial.mainTexture = usedTexture2;
            Debug.Log("Se colocó la textura 2");
        }
        instantiatedPrefab.AddComponent<MeshRenderer>().material = usedMaterial;

        
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
    }

}