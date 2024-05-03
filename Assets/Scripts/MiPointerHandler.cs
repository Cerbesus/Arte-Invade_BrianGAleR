using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;

public class MiPointerHandler : MonoBehaviour
{
    public GameObject ventana;
    public float distance;
    public Vector3 offset;

    private void Update()
    {
        if (CameraCache.Main != null)
        {
            // Calcular la dirección hacia la cámara desde la posición actual del menú
            Vector3 lookAtDirection = CameraCache.Main.transform.position - ventana.transform.position;

            // Rotar la ventana para que mire hacia la cámara
            Quaternion targetRotation = Quaternion.LookRotation(lookAtDirection);

            // Mantener la rotación en el eje Y para mantener la orientación correcta
            targetRotation *= Quaternion.Euler(0, 180, 0);

            // Obtener la posición de la cámara con un offset opcional
            Vector3 desiredPosition = CameraCache.Main.transform.position + 
                CameraCache.Main.transform.TransformDirection(offset);

            // Establecer la posición deseada para la ventana
            ventana.transform.position = desiredPosition;

            // Establecer la rotación deseada para la ventana
            ventana.transform.rotation = targetRotation;
        }
    }
}
