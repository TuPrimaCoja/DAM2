using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class showPasswordScript : MonoBehaviour
{
    public TMP_InputField passwordTextField;
    public void showPassword()
    {
        if (passwordTextField.contentType == TMP_InputField.ContentType.Password)
        {
            passwordTextField.contentType = TMP_InputField.ContentType.Standard;
        }
        else
        {
            passwordTextField.contentType = TMP_InputField.ContentType.Password;
        }
        passwordTextField.ForceLabelUpdate();
    }
}
