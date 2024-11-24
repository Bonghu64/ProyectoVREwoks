using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class FireManager : MonoBehaviour
{
    public Material fireSkybox;        // Skybox cuando hay incendios
    public Material clearSkybox;      // Skybox cuando no hay incendios
    public AudioClip fireSound;       // Sonido ambiental cuando hay incendios
    public AudioClip clearSound;      // Sonido ambiental cuando no hay incendios
    public AudioSource audioSource;   // AudioSource para los sonidos ambientales

    private int activeFires;          // Contador de incendios activos
    private FireEffect[] fireEffects; // Lista de efectos de fuego en la escena

    private void Start()
    {
        // Encuentra todos los efectos de fuego en el escenario
        fireEffects = FindObjectsOfType<FireEffect>();
        activeFires = fireEffects.Length;

        // Configura el Skybox inicial y el sonido
        RenderSettings.skybox = fireSkybox;
        PlaySound(fireSound);
    }

    public void OnFireExtinguished()
    {
        activeFires--;

        if (activeFires <= 0)
        {
            // Cambiar Skybox y sonido cuando no queden incendios
            RenderSettings.skybox = clearSkybox;
            PlaySound(clearSound);
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.Stop();
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}
