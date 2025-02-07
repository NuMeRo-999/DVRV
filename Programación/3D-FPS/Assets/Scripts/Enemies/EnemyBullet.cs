using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 30f;
    public float lifetime = 3f;
    public int damage = 10;
    public LayerMask hitLayers;
    public ParticleSystem impactEffect;
    public GameObject bulletTrailPrefab;

    private Rigidbody rb;
    private GameObject bulletTrailInstance;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.linearVelocity = transform.forward * speed;

        if (bulletTrailPrefab)
        {
            bulletTrailInstance = Instantiate(bulletTrailPrefab, transform.position, Quaternion.identity);
            bulletTrailInstance.transform.SetParent(transform);
        }

        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }

        if (((1 << other.gameObject.layer) & hitLayers) != 0)
        {
            if (impactEffect)
            {
                ParticleSystem impact = Instantiate(impactEffect, transform.position, Quaternion.identity);
                Destroy(impact.gameObject, 1.5f);
            }

            DestroyBullet();
        }
    }

    private void DestroyBullet()
    {
        if (bulletTrailInstance)
        {
            bulletTrailInstance.transform.SetParent(null);
            Destroy(bulletTrailInstance, 0.5f);
        }

        Destroy(gameObject);
    }
}
