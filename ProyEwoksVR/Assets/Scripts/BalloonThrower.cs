using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonThrower : MonoBehaviour
{
    public GameObject balloonPrefab; // Prefab del globo
    public Transform holdPoint;      // Punto donde el globo se mostrará al costado del jugador
    public Transform throwPoint;     // Punto desde donde se lanzará el globo
    public int maxBalloons = 6;      // Límite de globos
    private int currentBalloons;

    private GameObject currentBalloon; // Referencia al globo preparado

    private void Start()
    {
        currentBalloons = maxBalloons;
    }

    public void PrepareBalloon()
    {
        if (currentBalloons > 0 && currentBalloon == null)
        {
            // Instancia el globo en el punto de espera
            currentBalloon = Instantiate(balloonPrefab, holdPoint.position, holdPoint.rotation);
            currentBalloon.GetComponent<Rigidbody>().isKinematic = true; // Evita que caiga antes de lanzar
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
}
