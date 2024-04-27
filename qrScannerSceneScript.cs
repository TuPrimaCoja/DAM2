using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using ZXing;
using UnityEngine.SceneManagement;

public class qrScannerScene : MonoBehaviour
{
    WebCamTexture webcamTexture;
    public string QrCode = string.Empty;
    private characterLoaderScript cls;
    private bool qrFound = false;

    void Start()
    {
        var renderer = GetComponent<RawImage>();
        webcamTexture = new WebCamTexture(1920, 1080);

        // Ajustar la rotación de la textura de la cámara
        webcamTexture.deviceName = GetDeviceName();
        webcamTexture.Play();
        renderer.texture = webcamTexture;

        // Aplicar rotación de la textura
        renderer.rectTransform.localEulerAngles = new Vector3(0, 180, -270);

        cls = new characterLoaderScript();
        StartCoroutine(GetQRCode());
        Debug.Log("Started QR CODE READER");
    }

    string GetDeviceName()
    {
        foreach (WebCamDevice device in WebCamTexture.devices)
        {
            if (!device.isFrontFacing)
                return device.name; // Usar la cámara trasera si está disponible
        }
        return WebCamTexture.devices[0].name;
    }

    IEnumerator GetQRCode()
    {
        IBarcodeReader barCodeReader = new BarcodeReader();
        var snap = new Texture2D(webcamTexture.width, webcamTexture.height, TextureFormat.ARGB32, false);
        while (!qrFound)
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
                        qrFound = true;
                        // Desactivar la cámara
                        webcamTexture.Stop();
                        // Cargar la nueva escena y descartar la actual
                        SceneManager.LoadScene("CharacterScene");
                    }
                }
            }
            catch (Exception ex) { Debug.LogWarning(ex.Message); }
            yield return null;
        }
    }

    private void OnGUI()
    {
        int w = Screen.width, h = Screen.height;

        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(0, 0, w, h * 2 / 100);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 2 / 50;
        style.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);
        string text = QrCode;
        GUI.Label(rect, text, style);
    }
}