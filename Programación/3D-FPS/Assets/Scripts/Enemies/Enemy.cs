using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    private NavMeshAgent navMeshAgent;
    private Animator animator;

    public GameObject meatGenerator;
    public bool isDead = false;

    public float health = 100f;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        navMeshAgent.SetDestination(player.transform.position);
        animator.SetBool("Running", true);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        if (meatGenerator)
        {
            print("Generating meat");
            if (!isDead)
            {
                Instantiate(meatGenerator, transform.position + Vector3.up, Quaternion.identity);
                isDead = true;
            }
        }

        Destroy(gameObject);
    }

    
}
