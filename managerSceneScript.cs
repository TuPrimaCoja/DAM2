using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class managerSceneScript : MonoBehaviour
{
    public void loadScene(string scene)
    {
        Debug.Log("Cambiando a escena: " + scene);
        SceneManager.LoadScene(scene);
    }

    public void quitGame()
    {
        Debug.Log("Saliendo del juego");
        Application.Quit();
    }
}