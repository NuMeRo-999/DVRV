using System.Collections;
using UnityEngine;

public class ShotgunPellet : MonoBehaviour
{
    public float speed = 50f;
    public float lifetime = 2f;
    public int damage = 10;
    public LayerMask hitLayers;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = -transform.forward * speed;

        // Destruir la bala después de un tiempo
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & hitLayers) != 0)
        {
            // Si el objeto impactado tiene un script de salud, aplicamos daño
            // Health targetHealth = other.GetComponent<Health>();
            // if (targetHealth != null)
            // {
            //     targetHealth.TakeDamage(damage);
            // }

            // Destruir la bala al impactar
            Destroy(gameObject);
        }
    }
}
