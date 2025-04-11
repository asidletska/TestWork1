using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int damage = 20;
    public ParticleSystem blood;

    private void OnCollisionEnter(Collision collision)
    {
        Enemy targetEnemy = collision.collider.GetComponent<Enemy>();

        if (targetEnemy != null)
        {
            targetEnemy.TakeDamage(damage);

            if (blood != null)
            {
                ParticleSystem ps = Instantiate(blood, collision.contacts[0].point, Quaternion.identity);
                ps.Play();
            }

        }
        Destroy(gameObject, 5);
    }
}
