using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public int spawnIndex;
    public EnemySpawner spawner;
    public float detectionRange = 30f;
    public float throwRange = 10f;
    public float throwInterval = 3f;
    private int health = 50;

    [Header("Throwing")]
    public Transform throwPoint;
    public GameObject weaponPrefab;
    public float throwForce = 18f;

    private Animator animator;
    private float throwCooldown = 2;

    private NavMeshAgent agent;
    private bool isDead = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        throwCooldown = throwInterval;
    }

    void Update()
    {
        if (player == null || isDead) return;

        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance <= detectionRange)
        {
            if (distance > throwRange)
            {
                agent.SetDestination(player.position);
                animator.SetFloat("isRunning", 1);
            }
            else
            {
                agent.SetDestination(transform.position);
                animator.SetFloat("isRunning", 0);
                LookAtPlayer();

                throwCooldown -= Time.deltaTime;
                if (throwCooldown <= 0f)
                {
                    ThrowWeapon();
                    throwCooldown = throwInterval;
                }
            }
        }
    }
    void LookAtPlayer()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        direction.y = 0;
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }

    public void ThrowWeapon()
    {
        animator.SetTrigger("Throw");
        Invoke(nameof(SpawnWeapon), 0.5f);
    }
    void SpawnWeapon()
    {
        GameObject weapon = Instantiate(weaponPrefab, throwPoint.position, throwPoint.rotation);
        Rigidbody rb = weapon.GetComponent<Rigidbody>();
        Vector3 direction = (player.transform.position + Vector3.up * 1.5f - throwPoint.position).normalized;
        rb.AddForce(direction * throwForce, ForceMode.Impulse);
    }
    public void TakeDamage(int damage)
    {
        if (isDead) return;
        health -= damage;

        if (health <= 0)
        {
            animator.SetTrigger("Die");
            Die();
        }
    }

    public void Die()
    {
        animator.SetTrigger("Die");
        isDead = true;
        agent.enabled = false;
        Destroy(gameObject, 3);
        GameManager.instance.AddKill();
        spawner.FreeSpawnPoint(spawnIndex);
    }


}

