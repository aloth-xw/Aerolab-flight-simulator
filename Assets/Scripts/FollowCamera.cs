using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField]private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0, 3, -10);
    [SerializeField] private float positionSmoothTime = 0.15f;
    [SerializeField] private float rotationSmoothSpeed = 3f;

    [SerializeField] private ShakeCamera shakeCamera;

    private Vector3 velocityRef;

    private void LateUpdate()
    {
        if (target ==null) return;
        
        Vector3 desiredPosition = target.position + target.TransformDirection(offset);
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocityRef, positionSmoothTime);

        Quaternion desiredRotation = Quaternion.LookRotation(target.forward, target.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSmoothSpeed * Time.deltaTime);
    
         if (shakeCamera != null)
    {
        transform.position += transform.TransformDirection(shakeCamera.CurrentPositionOffset);
        transform.rotation *= shakeCamera.CurrentRotationOffset;
    }
    }
}
