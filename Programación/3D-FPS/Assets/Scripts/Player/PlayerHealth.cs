using UnityEngine;
using UnityEngine.Rendering;

public class PlayerHealth : MonoBehaviour
{

    public int health = 100;
    public int maxHealth = 100;
    public GameObject gameOverPanel;
    public Volume volume;


    void Start()
    {
        gameOverPanel.SetActive(false);
        volume.enabled = false;
    }

    void Update()
    {
        
    }

    public void Heal(int heal)
    {
        health += heal;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gameOverPanel.SetActive(true);
        GetComponent<PlayerMovement>().enabled = false;
        volume.enabled = true;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Blood")) {
            Invoke("Die", 2f);
        }
    }
}
