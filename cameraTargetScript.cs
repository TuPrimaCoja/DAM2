using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraTargetScript : MonoBehaviour
{
    public Transform target; // El GameObject vacío alrededor del cual quieres rotar la cámara
    public float rotationSpeed = 5f; // Velocidad de rotación

    private bool isRotating = false; // Variable para verificar si la rotación está activa
    private Vector2 lastTouchPosition; // Última posición de contacto

    void Update()
    {
        // Manejar entrada táctil para rotación
        if (Input.touchCount == 2)
        {
            // Obtener los dos toques
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            // Verificar si ambos toques están comenzando (no en movimiento)
            if (touch1.phase == TouchPhase.Began && touch2.phase == TouchPhase.Began)
            {
                // Activar la rotación
                isRotating = true;
                lastTouchPosition = (touch1.position + touch2.position) / 2f; // Guardar la posición media de los dos toques
            }
        }
        else if (Input.touchCount == 0)
        {
            // Desactivar la rotación si no hay toques
            isRotating = false;
        }

        // Rotar la cámara si la rotación está activa
        if (isRotating)
        {
            // Calcular la diferencia de posición táctil desde el último frame
            Vector2 currentTouchPosition = (Input.GetTouch(0).position + Input.GetTouch(1).position) / 2f;
            float deltaX = currentTouchPosition.x - lastTouchPosition.x;
            float deltaY = currentTouchPosition.y - lastTouchPosition.y;

            // Calcular el ángulo de rotación
            float rotationX = -deltaY * rotationSpeed * Time.deltaTime;
            float rotationY = deltaX * rotationSpeed * Time.deltaTime;

            // Rotar la cámara alrededor del objetivo
            transform.RotateAround(target.position, Vector3.up, rotationY);
            transform.RotateAround(target.position, transform.right, rotationX);

            // Actualizar la última posición de contacto
            lastTouchPosition = currentTouchPosition;
        }
    }
}
