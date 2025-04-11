using UnityEngine;

public class weapon1 : MonoBehaviour
{
    public int damage = 20;
    public ParticleSystem blood;

    private void OnCollisionEnter(Collision collision)
    {
        Player targetPlayer = collision.collider.GetComponent<Player>();

        if (targetPlayer != null)
        {
            targetPlayer.TakeDamage(damage);

            if (blood != null)
            {
                ParticleSystem ps = Instantiate(blood, collision.contacts[0].point, Quaternion.identity);
                ps.Play();
            }
        }
        Destroy(gameObject, 5);
    }
}
