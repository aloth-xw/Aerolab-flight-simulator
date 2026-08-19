using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField]private Transform target;

    [SerializeField] private Vector3 offset = new Vector3(0, 3, -10);

    private void LateUpdate()
    {
        if (target ==null) return;
        transform.position = target.position + target.TransformDirection(offset);
        transform.LookAt(target);
    }
}
