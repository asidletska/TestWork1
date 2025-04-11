using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Health")]
    public float maxHealth = 100f;
    private float currentHealth;


    [Header("Animation")]
    private Animator animator;


    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    void Update()
    {

    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
            GameManager.instance.OnPlayerKilled();
        }
    }

    void Die()
    {
        animator.SetTrigger("Die");
        enabled = false;
    }
}
