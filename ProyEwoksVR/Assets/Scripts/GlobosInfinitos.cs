using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // Añadir referencia al nuevo sistema de entrada

public class GlobosInfinitos : MonoBehaviour
{
    public GameObject globoPrefab;  // Prefab del globo que se creará y aparecerá en la mano
    public Transform manoTransform; // Transform de la mano donde se colocará el globo
    public float fuerzaLanzamiento = 10f; // Fuerza de lanzamiento

    private GameObject globo;      // Instancia del globo en la escena

    private PlayerInput playerInput; // Referencia al sistema de entrada del jugador

    void Start()
    {
        // Verificar si los objetos esenciales están asignados
        if (globoPrefab == null)
        {
            Debug.LogError("El prefab del globo no está asignado. Por favor, asigna el prefab en el inspector.");
            return;
        }

        if (manoTransform == null)
        {
            Debug.LogError("El Transform de la mano no está asignado. Por favor, asigna el transform en el inspector.");
            return;
        }

        // Configurar el sistema de entrada
        playerInput = GetComponent<PlayerInput>();  // Obtener el componente PlayerInput (asegúrate de que el GameObject tenga este componente)
    }

    void Update()
    {
        // Detectar si se presiona la tecla "G" usando el nuevo sistema de Input
        if (Keyboard.current.gKey.wasPressedThisFrame && globo == null)
        {
            CrearGloboEnMano();  // Crear un globo solo si no existe uno ya
        }

        // Detectar si se presiona la tecla "G" para lanzar el globo
        if (Keyboard.current.gKey.wasPressedThisFrame && globo != null)
        {
            LanzarGlobo();
        }
    }

    void CrearGloboEnMano()
    {
        // Verificación para asegurarnos de que el prefab y el transform de la mano están disponibles
        if (globoPrefab == null || manoTransform == null)
        {
            Debug.LogError("Faltan referencias necesarias. El prefab del globo o el transform de la mano no están asignados.");
            return;
        }

        // Instanciar el globo en la posición de la mano
        globo = Instantiate(globoPrefab, manoTransform.position, manoTransform.rotation);

        // Verificamos si la instancia del globo es null
        if (globo == null)
        {
            Debug.LogError("No se pudo instanciar el globo. Verifica que el prefab esté correctamente asignado.");
            return;
        }

        // NO hacemos que el globo sea hijo de la mano (Eliminamos esta parte)
        // globo.transform.SetParent(manoTransform);

        // Ajustar la posición y rotación del globo en relación a la mano
        globo.transform.position = manoTransform.position; // Mantener la posición de la mano, pero no seguirla
        globo.transform.rotation = manoTransform.rotation; // Mantener la rotación de la mano, pero no seguirla

        // Añadir el componente XRGrabInteractable para que el globo sea interactuable
        UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable = globo.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        if (grabInteractable == null)
        {
            grabInteractable = globo.AddComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        }

        // Verificar que XRGrabInteractable se ha añadido correctamente
        if (grabInteractable == null)
        {
            Debug.LogError("No se pudo añadir el componente XRGrabInteractable al globo.");
            return;
        }

        // Añadir Rigidbody si no tiene uno
        Rigidbody rb = globo.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = globo.AddComponent<Rigidbody>();
        }

        // Verificar que Rigidbody se añadió correctamente
        if (rb == null)
        {
            Debug.LogError("No se pudo añadir el componente Rigidbody al globo.");
            return;
        }

        // Configurar el Rigidbody
        rb.useGravity = true;  // Activar la gravedad para que el globo caiga
        rb.isKinematic = false; // Desactivar el modo cinemático para permitir la física

        Debug.Log("Globo instanciado y configurado correctamente.");
    }

    void LanzarGlobo()
    {
        // Si el globo tiene un Rigidbody, lanzarlo
        Rigidbody rb = globo.GetComponent<Rigidbody>();

        if (rb != null)
        {
            // Aplicar una fuerza en la dirección hacia donde está mirando la mano
            Vector3 direccionLanzamiento = manoTransform.forward;  // Dirección de la mano (hacia donde apunta)
            rb.AddForce(direccionLanzamiento * fuerzaLanzamiento, ForceMode.VelocityChange);

            // Desvincular el globo de la mano después de lanzarlo (aunque ya no es hijo de la mano)
            // globo.transform.SetParent(null);  // No es necesario, ya no es hijo

            // Opcional: Destruir el globo después de cierto tiempo (para limpiar la escena)
            Destroy(globo, 2f);  // El globo se destruye después de 2 segundos
            globo = null; // Asegurarse de que globo esté en null para que no intente lanzar otro inmediatamente
        }
    }
}
