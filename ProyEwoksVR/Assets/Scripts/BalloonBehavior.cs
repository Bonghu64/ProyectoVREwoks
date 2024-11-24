using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject burstEffectPrefab; // Prefab del efecto de reventar (Particle System)

    private void OnCollisionEnter(Collision collision)
    {
        // Instanciar el efecto de reventar en el punto de colisión
        Instantiate(burstEffectPrefab, collision.contacts[0].point, Quaternion.identity);

        // Destruir el globo
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Alternativa para Triggers
        Instantiate(burstEffectPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
