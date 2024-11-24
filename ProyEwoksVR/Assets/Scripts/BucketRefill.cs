using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketRefill : MonoBehaviour
{
    public Transform player; // Asigna manualmente el jugador
    public float refillDistance = 2f; // Distancia mínima para recargar
    public BalloonThrower balloonThrower;

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= refillDistance)
        {
            if (balloonThrower != null)
            {
                balloonThrower.RefillBalloons();
                Debug.Log("Globos recargados!");
            }
        }
    }

}
