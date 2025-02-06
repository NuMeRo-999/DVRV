using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    private NavMeshAgent navMeshAgent;
    private Animator animator;

    public GameObject meatGenerator;

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

        }
    }

    public void Die()
    {
        if (meatGenerator)
        {
            Instantiate(meatGenerator, transform.position, Quaternion.identity);
        }
        
        Destroy(gameObject);
    }
}
