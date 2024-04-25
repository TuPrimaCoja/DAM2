using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class rememberMeScript : MonoBehaviour
{
    public TMP_InputField userTextField;
    public TMP_InputField passwordTextField;                                                                                        
    public Toggle rememberToggle; // esto es checkBox
    private string filePath;
    private string fileName = "accountSettings.json";
    [System.Serializable]
    public class AccountSettings
    {
        public string user;
        public string password;
        public bool remember;
    }

    AccountSettings accountSettings = new AccountSettings();

    void Start()
    {
        filePath = Path.Combine(Application.persistentDataPath, fileName);
        LoadTextFromFile();
    }

    //Para guardar el texto en el block de notas si está activado el checkBox
    public void SaveTextToFile()
    {
        accountSettings.user = userTextField.text;
        accountSettings.password = passwordTextField.text;
        accountSettings.remember = rememberToggle.isOn;

        string json = JsonUtility.ToJson(accountSettings);
        File.WriteAllText(filePath, json);

        Debug.Log("Nuevo texto guardado en: " + filePath);
    }

    //Para cargar o eliminar el texto en los textField de usuario y contraseña según el estado de la checkBox
    private void LoadTextFromFile()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            accountSettings = JsonUtility.FromJson<AccountSettings>(json);

            userTextField.text = accountSettings.user;
            passwordTextField.text = accountSettings.password;
            rememberToggle.isOn = accountSettings.remember;

            Debug.Log("Texto cargado desde : " + filePath + ", Usuario : " + userTextField.text + ", Contraseña : " + passwordTextField.text + ", Estado del CheckBox: " + rememberToggle.isOn);

            if (!rememberToggle.isOn)
            {
                userTextField.text = "";
                passwordTextField.text = "";
            }
        }
        else
        {
            Debug.Log("No hubo ningún texto que cargar desde: " + filePath);
        }
    }
}