using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Networking;
using TMPro;
using System.Text.RegularExpressions;

public class loginScript : MonoBehaviour
{
    [SerializeField] nonVolatileData fakeSingleton;
    public TMP_InputField inputField1;
    public TMP_InputField inputField2;
    public string urlValidaUsuario = "http://www.ieslassalinas.org/APP/appValidaUsuario.php";
    public void CompareFields()
    {
        StartCoroutine(SendDataToPHP());
    }

    // Para hacer el inicio de sesion del alumno
    IEnumerator SendDataToPHP()
    {
        WWWForm form = new WWWForm();
        form.AddField("usuario", inputField1.text);
        form.AddField("contrasena", inputField2.text);
       
        using (UnityWebRequest www = UnityWebRequest.Post(urlValidaUsuario, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(www.error);
            }
            else
            {
                string responseText = www.downloadHandler.text;
                responseText = Regex.Replace(responseText, @"[^a-zA-Z0-9:\-{},;"" ]", "");
                if (!responseText.Equals("0"))
                {
                    Debug.Log("Usuario encontrado");
                    fakeSingleton.SetDNI(inputField1.text);
                    Debug.Log("NIE: " + fakeSingleton.GetDNI());
                    SceneManager.LoadScene("secondMenuScene");

                }
                else
                {
                    Debug.Log("Usuario no encontrado");
                }
                Debug.Log("Comparación resultante: " + responseText);
            }
        }
    }
}