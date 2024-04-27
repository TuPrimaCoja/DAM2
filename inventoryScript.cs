using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inventoryScript : MonoBehaviour
{
    public RawImage rawImage1;
    public RawImage rawImage2;
    public RawImage rawImage3;
    public RawImage rawImage4;
    public RawImage rawImage5;
    public RawImage rawImage6;
    public RawImage rawImage7;
    public RawImage rawImage8;
    public RawImage rawImage9;
    public RawImage rawImage10;
    public RawImage rawImage11;
    public RawImage rawImage12;
    public RawImage rawImage13;
    public RawImage rawImage14;
    public RawImage rawImage15;
    public RawImage rawImage16;
    public RawImage rawImage17;
    public RawImage rawImage18;
    public RawImage rawImage19;
    public RawImage rawImage20;
    public RawImage rawImage21;
    public RawImage rawImage22;
    public RawImage rawImage23;
    public RawImage rawImage24;
    public RawImage rawImage25;

    int d = 11; 
    public RawImage[] rawImages = new RawImage[25];
    void Start()
    {









        // Asignar imágenes a través del array
        for (int i = 0; i < d; i++)
        {
            string imagePath = "Assets/images/itemImages/image" + (i + 1); // Suponiendo nombres de imágenes secuenciales (miImagen1, miImagen2, etc.)
            Texture2D texture = Resources.Load<Texture2D>(imagePath);

            if (texture != null)
            {
                // Asignar la textura al RawImage correspondiente en el array
                rawImages[i].texture = texture;
            }
            else
            {
                Debug.LogError("No se pudo cargar la imagen desde: " + imagePath);
            }
        }
    }
}

