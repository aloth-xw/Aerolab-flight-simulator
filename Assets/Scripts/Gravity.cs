using UnityEngine;

public class Gravity : MonoBehaviour
{
    [SerializeField]
    private Vector3 gravity = new Vector3(0f, -9.81f, 0f);

    public Vector3 GetGravity()
    {
        return gravity;
    }
}
