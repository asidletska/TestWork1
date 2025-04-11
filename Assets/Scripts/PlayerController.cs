using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]private Rigidbody rb;
    [SerializeField]private FixedJoystick joint;
    [SerializeField] private Animator animator;

    private float moveSpeed = 4;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector3(joint.Horizontal * moveSpeed, rb.velocity.y, joint.Vertical * moveSpeed);

        if(joint.Horizontal != 0 || joint.Vertical !=0)
        {
            transform.rotation = Quaternion.LookRotation(rb.velocity);
            animator.SetFloat("isRunning", moveSpeed);
        }
        else
        {
            animator.SetFloat("isRunning", 0);
        }
    }
}
