using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ShotgunPellet : MonoBehaviour
{
    public float speed = 50f;
    public float lifetime = 2f;
    public int damage = 10;
    public LayerMask hitLayers;
    public ParticleSystem impactEffect; // Prefab del sistema de partículas
    public GameObject bulletTrailPrefab;

    private Rigidbody rb;
    private GameObject bulletTrailInstance;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = -transform.forward * speed;

        // Instanciar el rastro de la bala
        if (bulletTrailPrefab)
        {
            bulletTrailInstance = Instantiate(bulletTrailPrefab, transform.position, Quaternion.identity);
            bulletTrailInstance.transform.SetParent(transform); // Que siga la bala
        }

        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // if(collision.gameObject.CompareTag("Ground"))
        if (((1 << collision.gameObject.layer) & hitLayers) != 0)
    {
        ContactPoint contact = collision.contacts[0]; // Primer punto de contacto
    
        // Crear el efecto de impacto en el punto exacto con la rotación adecuada
        if (impactEffect)
        {
            ParticleSystem impact = Instantiate(impactEffect, contact.point, Quaternion.LookRotation(contact.normal));
            Destroy(impact.gameObject, 1.5f); // Destruir después de 1.5s
        }
    
        DestroyBullet();
    }
    }

    private void DestroyBullet()
    {
        if (bulletTrailInstance)
        {
            bulletTrailInstance.transform.SetParent(null); // Liberar el trail para que termine
            Destroy(bulletTrailInstance, 0.5f); // Darle tiempo al trail para desvanecerse
        }

        Destroy(gameObject);
    }
}
