using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MenuManagerXR : MonoBehaviour
{
    // Referencia al GameObject que contiene el LocomotionSystem
    public GameObject locomotionObject; // El GameObject que contiene el LocomotionSystem
    public GameObject menuCanvas;       // El Canvas que queremos desactivar

    // Función que se llama al presionar el botón para empezar el movimiento
    public void StartButton()
    {
        // Desactivar el Canvas del menú
        menuCanvas.SetActive(false);

        // Activar el GameObject que contiene el LocomotionSystem
        if (locomotionObject != null)
        {
            locomotionObject.SetActive(true);  // Activa el GameObject que contiene el LocomotionSystem
        }
        else
        {
            Debug.LogError("locomotionObject no asignado.");
        }
    }

    // Función para terminar el juego
    public void ExitButton()
    {
        // Salir del juego (solo en versión compilada)
        Application.Quit();

        // Si estás en el editor de Unity, detener la reproducción
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
