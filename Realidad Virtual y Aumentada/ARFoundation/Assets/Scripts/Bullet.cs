using UnityEngine;

public class Bullet : MonoBehaviour
{
    public ParticleSystem FoodParticleSystem;

    void Start()
    {

    }

    void Update()
    {
        transform.Rotate(0, 0, 360 * Time.deltaTime);
        Destroy(gameObject, 3f);
    }

    void OnCollisionEnter(Collision collision)
    {
        Instantiate(FoodParticleSystem, transform.position, Quaternion.identity);
        FoodParticleSystem.Play();
        Destroy(collision.gameObject);
        Destroy(gameObject);
    }
}
