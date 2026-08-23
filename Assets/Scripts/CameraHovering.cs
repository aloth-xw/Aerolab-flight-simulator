using UnityEngine;

public class CameraHovering : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float distance = 12f;
    [SerializeField] private float height = 2.5f;

    private float currentAngle = 0f;

    private void LateUpdate()
    {
        if (target == null)
        {
            currentAngle += rotationSpeed*Time.deltaTime;

            Quaternion rotation = Quaternion.Euler(0f,currentAngle,0f);

            Vector3 positionOffset = rotation * new Vector3(0,height,-distance);

            transform.position = target.position + positionOffset;
            transform.LookAt(target.position + Vector3.up * 1f);
        }
    }
}
