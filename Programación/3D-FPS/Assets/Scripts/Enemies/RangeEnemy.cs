using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class RangeEnemy : MonoBehaviour
{
    public GameObject player;
    private NavMeshAgent navMeshAgent;
    private Animator animator;

    public GameObject meatGenerator;
    public bool isDead = false;
    public float health = 100f;

    public enum NPCState { Idle, Alert, Attack }
    public NPCState currentState = NPCState.Idle;

    [Header("Player Tracking")]
    [SerializeField] private float playerDetectionRange = 15f;
    [SerializeField] private float attackRange = 10f;
    [SerializeField] private float stopChaseDistance = 7f;
    [SerializeField] private float angleVision = 60f;

    [Header("Shooting Settings")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float projectileSpeed = 20f;
    private bool canShoot = true;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        navMeshAgent.isStopped = true;
        navMeshAgent.stoppingDistance = stopChaseDistance;
    }

    void Update()
    {
        switch (currentState)
        {
            case NPCState.Idle:
                DetectPlayer();
                break;
            case NPCState.Alert:
                ChasePlayer();
                break;
            case NPCState.Attack:
                Attack();
                break;
        }
    }

    private void DetectPlayer()
    {
        Vector3 directionToPlayer = player.transform.position - transform.position;

        if (Vector3.Distance(transform.position, player.transform.position) < playerDetectionRange &&
            Vector3.Angle(transform.forward, directionToPlayer) < angleVision)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, directionToPlayer, out hit, playerDetectionRange))
            {
                if (hit.collider.gameObject == player)
                {
                    currentState = NPCState.Alert;
                    navMeshAgent.isStopped = false;
                    animator.SetBool("Running", true);
                }
            }
        }
    }

    private void ChasePlayer()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > stopChaseDistance)
        {
            navMeshAgent.SetDestination(player.transform.position);
        }
        else
        {
            navMeshAgent.ResetPath();
        }

        if (Vector3.Distance(transform.position, player.transform.position) <= attackRange)
        {
            currentState = NPCState.Attack;
            animator.SetBool("Running", false);
        }
        else if (Vector3.Distance(transform.position, player.transform.position) > playerDetectionRange)
        {
            currentState = NPCState.Idle;
            navMeshAgent.isStopped = true;
            animator.SetBool("Running", false);
        }
    }

    private void Attack()
    {
        transform.LookAt(player.transform);

        if (Vector3.Distance(transform.position, player.transform.position) > attackRange)
        {
            currentState = NPCState.Alert;
            return;
        }

        if (canShoot)
        {
            canShoot = false;
            animator.SetTrigger("Shoot");
            StartCoroutine(ShootProjectile());
        }
    }

    private IEnumerator ShootProjectile()
    {
        yield return new WaitForSeconds(0.5f); // Simula el tiempo de la animación de disparo

        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = firePoint.forward * projectileSpeed;
        }

        yield return new WaitForSeconds(fireRate);
        canShoot = true;
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
        if (meatGenerator && !isDead)
        {
            Instantiate(meatGenerator, transform.position + Vector3.up, Quaternion.identity);
            isDead = true;
        }
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerDetectionRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Vector3 frontRayPoint = transform.position + (transform.forward * playerDetectionRange);
        Vector3 leftRayPoint = Quaternion.Euler(0, angleVision * 0.5f, 0) * frontRayPoint;
        Vector3 rightRayPoint = Quaternion.Euler(0, -angleVision * 0.5f, 0) * frontRayPoint;

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, frontRayPoint);
        Gizmos.DrawLine(transform.position, leftRayPoint);
        Gizmos.DrawLine(transform.position, rightRayPoint);
    }
}
