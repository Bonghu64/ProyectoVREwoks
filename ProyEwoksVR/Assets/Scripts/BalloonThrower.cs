using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class BalloonThrower : MonoBehaviour
{
    public GameObject balloonPrefab; // Prefab del globo
    public Transform holdPoint;      // Punto donde el globo se mostrará al costado del jugador
    public Transform throwPoint;     // Punto desde donde se lanzará el globo
    public int maxBalloons = 6;      // Límite de globos
    private int currentBalloons;

    private GameObject currentBalloon; // Referencia al globo preparado

    // Referencias a las Input Actions
    public InputActionReference prepareBalloonAction; // Acción para preparar el globo
    public InputActionReference throwBalloonAction;   // Acción para lanzar el globo

    private void Start()
    {
        currentBalloons = maxBalloons;
    }

    private void OnEnable()
    {
        // Vincular las acciones a los métodos
        prepareBalloonAction.action.performed += OnPrepareBalloon;
        throwBalloonAction.action.performed += OnThrowBalloon;

        // Activar las acciones
        prepareBalloonAction.action.Enable();
        throwBalloonAction.action.Enable();
    }

    private void OnDisable()
    {
        // Desvincular las acciones al deshabilitar
        prepareBalloonAction.action.performed -= OnPrepareBalloon;
        throwBalloonAction.action.performed -= OnThrowBalloon;

        // Desactivar las acciones
        prepareBalloonAction.action.Disable();
        throwBalloonAction.action.Disable();
    }

    private void OnPrepareBalloon(InputAction.CallbackContext context)
    {
        PrepareBalloon();
    }

    private void OnThrowBalloon(InputAction.CallbackContext context)
    {
        ThrowBalloon();
    }

    public void PrepareBalloon()
    {
        if (currentBalloons > 0 && currentBalloon == null)
        {
            // Instancia el globo en el punto de espera
            currentBalloon = Instantiate(balloonPrefab, holdPoint.position, holdPoint.rotation);
            currentBalloon.GetComponent<Rigidbody>().isKinematic = true; // Evita que caiga antes de lanzar

            // Asigna el globo como hijo del holdPoint para que siga su posición
            currentBalloon.transform.SetParent(holdPoint);
        }
        else if (currentBalloons <= 0)
        {
            Debug.Log("No quedan globos!");
        }
    }


    public void ThrowBalloon()
    {
        if (currentBalloon != null)
        {
            // Elimina la relación padre
            currentBalloon.transform.SetParent(null);

            // Mueve el globo al punto de lanzamiento
            currentBalloon.transform.position = throwPoint.position;
            currentBalloon.transform.rotation = throwPoint.rotation;

            // Activa la física y lanza el globo
            Rigidbody rb = currentBalloon.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.AddForce(throwPoint.forward * 500f); // Ajusta la fuerza según sea necesario

            currentBalloon = null; // Elimina la referencia al globo actual
            currentBalloons--;    // Reduce la cantidad de globos disponibles
        }
    }


    public void RefillBalloons()
    {
        currentBalloons = maxBalloons; // Recarga globos
    }

    private void Update()
    {
        if (currentBalloon != null)
        {
            // Actualiza la posición y rotación del globo al holdPoint
            currentBalloon.transform.position = holdPoint.position;
            currentBalloon.transform.rotation = holdPoint.rotation;
        }
    }

}