using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using ZXing;
using UnityEngine.SceneManagement;
using TMPro;

public class qrScannerScript : MonoBehaviour
{
    [SerializeField] nonVolatileData fakeSingleton;
    WebCamTexture webcamTexture;
    public TMP_Text qrLabel;
    string QrCode = string.Empty;
    IBarcodeReader barCodeReader = null;
    private string goBackSceneName = "secondMenuScene";
    void Start()
    {
        var renderer = GetComponent<RawImage>();
        webcamTexture = new WebCamTexture(512, 512);
        renderer.texture = webcamTexture;
        StartCoroutine(GetQRCode());
    }
    IEnumerator GetQRCode()
    {
        barCodeReader = new BarcodeReader();
        webcamTexture.Play();
        var snap = new Texture2D(webcamTexture.width, webcamTexture.height, TextureFormat.ARGB32, false);
        while (string.IsNullOrEmpty(QrCode))
        {
            try
            {
                snap.SetPixels32(webcamTexture.GetPixels32());
                var Result = barCodeReader.Decode(snap.GetRawTextureData(), webcamTexture.width, webcamTexture.height, RGBLuminanceSource.BitmapFormat.ARGB32);
                if (Result != null)
                {
                    QrCode = Result.Text;
                    if (!string.IsNullOrEmpty(QrCode))
                    {
                        qrLabel.text = "NIE escaneado : " + QrCode;
                        fakeSingleton.SetDNI(QrCode);
                        Debug.Log("NIE (Desde QR): " + fakeSingleton.GetDNI());
                        webcamTexture.Stop();
                        barCodeReader = null;
                        SceneManager.LoadScene("characterScene");
                        break;
                    }
                }
            }
            catch (Exception ex) { Debug.LogWarning(ex.Message); }
            //yield return null;
            yield return new WaitForSeconds(0.25f);
        }
    }
    public void CloseWebcam()
    {
        webcamTexture.Stop();
        barCodeReader = null;
        Debug.Log("Cambiando a escena: " + goBackSceneName);
        SceneManager.LoadScene(goBackSceneName);
    }
    
    /*
    public void OnGUI()
    {
        int w = Screen.width, h = Screen.height;
        GUIStyle style = new GUIStyle();
        Rect rect = new Rect(0, 0, w, h * 2 / 100);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 2 / 50;
        style.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);
        string text =QrCode;
        GUI.Label(rect, text, style);
    }
    */
}
