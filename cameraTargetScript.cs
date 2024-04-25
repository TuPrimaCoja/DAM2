using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraTargetScript : MonoBehaviour
{
    public Transform target; // El GameObject vac�o alrededor del cual quieres rotar la c�mara
    public float rotationSpeed = 5f; // Velocidad de rotaci�n

    private bool isRotating = false; // Variable para verificar si la rotaci�n est� activa
    private Vector2 lastTouchPosition; // �ltima posici�n de contacto

    void Update()
    {
        // Manejar entrada t�ctil para rotaci�n
        if (Input.touchCount == 2)
        {
            // Obtener los dos toques
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            // Verificar si ambos toques est�n comenzando (no en movimiento)
            if (touch1.phase == TouchPhase.Began && touch2.phase == TouchPhase.Began)
            {
                // Activar la rotaci�n
                isRotating = true;
                lastTouchPosition = (touch1.position + touch2.position) / 2f; // Guardar la posici�n media de los dos toques
            }
        }
        else if (Input.touchCount == 0)
        {
            // Desactivar la rotaci�n si no hay toques
            isRotating = false;
        }

        // Rotar la c�mara si la rotaci�n est� activa
        if (isRotating)
        {
            // Calcular la diferencia de posici�n t�ctil desde el �ltimo frame
            Vector2 currentTouchPosition = (Input.GetTouch(0).position + Input.GetTouch(1).position) / 2f;
            float deltaX = currentTouchPosition.x - lastTouchPosition.x;
            float deltaY = currentTouchPosition.y - lastTouchPosition.y;

            // Calcular el �ngulo de rotaci�n
            float rotationX = -deltaY * rotationSpeed * Time.deltaTime;
            float rotationY = deltaX * rotationSpeed * Time.deltaTime;

            // Rotar la c�mara alrededor del objetivo
            transform.RotateAround(target.position, Vector3.up, rotationY);
            transform.RotateAround(target.position, transform.right, rotationX);

            // Actualizar la �ltima posici�n de contacto
            lastTouchPosition = currentTouchPosition;
        }
    }
}
