using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FireEffect : MonoBehaviour
{
    public ParticleSystem fireParticles; // El sistema de partículas que representa el fuego
    public AudioClip extinguishSound;    // Sonido de apagado de fuego

    private AudioSource audioSource;     // AudioSource para reproducir el sonido
    public FireManager fireManager; // Referencia al FireManager

    private void Start()
    {
        // Añade un AudioSource dinámicamente al objeto del fuego
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = extinguishSound;
        audioSource.playOnAwake = false; // Para que no suene automáticamente
        audioSource.spatialBlend = 1f;   // Para sonido 3D
        audioSource.volume = 2f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Balloon"))
        {
            // Detiene las partículas de fuego
            if (fireParticles != null)
            {
                fireParticles.Stop();
            }

            // Reproduce el sonido de apagado
            if (audioSource != null && extinguishSound != null)
            {
                audioSource.Play();
            }
            if (fireManager != null)
            {
                fireManager.OnFireExtinguished();
            }

            // Destruye el objeto de fuego después de un tiempo para permitir que el sonido termine
            Destroy(gameObject, 2f);
        }
    }
}
