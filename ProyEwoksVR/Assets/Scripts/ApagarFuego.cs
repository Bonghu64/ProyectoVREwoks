using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApagarFuego : MonoBehaviour
{
    // Asigna el objeto "Fuego" desde el Inspector
    public GameObject fuego;

    // Asigna el objeto "Globo de Agua" desde el Inspector
    public GameObject globoDeAgua;

    // Este método se llama cuando el collider de WaterBalloon (o cualquier objeto con collider) entra en contacto
    private void OnCollisionEnter(Collision collision)
    {
        // Si la colisión es con el "Globo de Agua"
        if (collision.gameObject == globoDeAgua)
        {
            // Desactiva el objeto "Fuego"
            fuego.SetActive(false);
            Debug.Log("¡El Fuego ha sido apagado por el Globo de Agua!");
        }
    }
}
