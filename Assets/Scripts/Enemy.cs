using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 15f;
    public float throwInterval = 3f;
    private int health = 100;

    [Header("Throwing")]
    public Transform throwPoint;
    public GameObject weaponPrefab;
    public float throwForce = 18f;

    private Animator animator;
    private float throwCooldown;

    void Start()
    {
        animator = GetComponent<Animator>();
        throwCooldown = throwInterval;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectionRange)
        {
            LookAtPlayer();

            throwCooldown -= Time.deltaTime;
            if (throwCooldown <= 0f)
            {
                ThrowWeapon();
                throwCooldown = throwInterval;
            }
        }
    }

    void LookAtPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0;
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }

    void ThrowWeapon()
    {
        animator.SetTrigger("Throw");

        GameObject weapon = Instantiate(weaponPrefab, throwPoint.position, throwPoint.rotation);
        Rigidbody rb = weapon.GetComponent<Rigidbody>();

        Vector3 direction = (player.position + Vector3.up * 1.5f - throwPoint.position).normalized;
        rb.AddForce(direction * throwForce, ForceMode.Impulse);
    }
    public void TakeDamage(int damage)
    {

        health -= damage;

        if (health <= 0)
        {
            Die();
            GameManager.instance.AddKill();
        }
    }

    void Die()
    {
        animator.SetTrigger("Die");
        enabled = false;
    }
}
