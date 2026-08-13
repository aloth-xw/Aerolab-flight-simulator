using UnityEngine;

public class PhysicsBody : MonoBehaviour
{
  private Rigidbody rb;

  private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public Vector3 GetCenterOfMass()
{
    return rb.worldCenterOfMass;
} 

  public Vector3 GetVelocity()
    {
        return   rb.linearVelocity;
    }

  public Vector3 GetPosition()
    {
        return transform.position;
    }

  public Vector3 GetForward()
    {
        return transform.forward;
    }

     public Vector3 GetUp()
    {
        return transform.up;
    }
     public Vector3 GetRight()
        {
            return transform.right;
        }

    public Vector3 GetAngularVelocity()
    {
        return rb.angularVelocity;
    }

    public void AddForce(Vector3 force)
        {
            rb.AddForce(force);
        }
    
    public void AddForceAtPosition(Vector3 force, Vector3 position)
    {
        rb.AddForceAtPosition(force, position);
    }
    public Vector3 GlobalToLocalDirection(Vector3 direction)
    {
        return transform.InverseTransformDirection(direction);
    }
    
    public Vector3 LocalToGlobalDirection(Vector3 direction)
    {
        return transform.TransformDirection(direction);
    }

    private void Start()
        {
            rb.linearVelocity = transform.forward * 40f;
        }

    public void FixedUpdate()
    {
        Debug.Log("Velocity: " + GetVelocity());
        Debug.Log("Inertia Tensor: " + rb.inertiaTensor);
    }

}
