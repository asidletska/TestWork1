using UnityEngine;

public class CameraFollow : MonoBehaviour
{  
    public Transform player;
    public Vector3 offset = new Vector3(0f, 5f, -7f); 
    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        Vector3 desiredPosition = player.transform.position + player.transform.rotation * offset;

        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        Quaternion desiredRotation = Quaternion.LookRotation(player.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, smoothSpeed * Time.deltaTime);
    }
}



