using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class PlayerThrow : MonoBehaviour
{

    public Transform throwPoint;
    public GameObject weaponPrefab;
    public float throwForce = 20f;
    public int trajectoryPoints = 30;
    public float timeStep = 0.1f;
    public LayerMask groundMask;

    private LineRenderer lineRenderer;
    private Vector3 throwDirection;
    private Animator animator;

    private bool isAiming = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began && !isAiming)
            {
                isAiming = true;
            }

            if (isAiming)
            {
                CalculateDirection(touch.position);
                ShowTrajectory();

                if (Input.touchCount > 1 || touch.phase == TouchPhase.Ended)
                {
                    animator.SetTrigger("Throw");
                    isAiming = false;
                    lineRenderer.positionCount = 0;
                }
            }
        }
    }

    void CalculateDirection(Vector2 screenPosition)
    {
        Vector3 touchWorld = GetTouchWorldPosition(screenPosition);
        throwDirection = (touchWorld - throwPoint.position).normalized;
    }

    void ShowTrajectory()
    {
        Vector3[] points = new Vector3[trajectoryPoints];
        Vector3 velocity = throwDirection * throwForce;
        Vector3 position = throwPoint.position;

        for (int i = 0; i < trajectoryPoints; i++)
        {
            points[i] = position;
            velocity += Physics.gravity * timeStep;
            position += velocity * timeStep;

            if (Physics.Raycast(position, velocity.normalized, out RaycastHit hit, velocity.magnitude * timeStep, groundMask))
            {
                points[i] = hit.point;
                lineRenderer.positionCount = i + 1;
                lineRenderer.SetPositions(points);
                return;
            }
        }

        lineRenderer.positionCount = trajectoryPoints;
        lineRenderer.SetPositions(points);
    }

    Vector3 GetTouchWorldPosition(Vector2 screenPos)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, groundMask))
        {
            return hit.point;
        }
        return throwPoint.position + throwPoint.forward * 5f;
    }

    public void ThrowWeapon()
    {
        GameObject weapon = Instantiate(weaponPrefab, throwPoint.position, throwPoint.rotation);
        Rigidbody rb = weapon.GetComponent<Rigidbody>();
        rb.AddForce(throwDirection * throwForce, ForceMode.Impulse);
    }
}
