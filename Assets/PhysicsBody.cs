using UnityEngine;
using UnityEngine.InputSystem;

public class PhysicsBody : MonoBehaviour
{
  private Rigidbody rb;
  
    private Vector3 previousVelocity;
    private Vector3 acceleration;
    [SerializeField] private float maxSafeAngularSpeed = 3f;
    //[SerializeField] private float emergencyDampingStrength = 500f;

  private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = Vector3.zero;
        rb.maxAngularVelocity = 15f;
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

    public Vector3 GetAcceleration()
    {
        return acceleration;
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

    public void SetVelocity(Vector3 velocity)
    {
        rb.linearVelocity = velocity;
    }

    public void AddTorque(Vector3 torque)
    {
        rb.AddTorque(torque);
    }

    

    private void Start()
        {
            transform.position = new Vector3(1666f, 3f, 1000f);
            rb.linearVelocity = transform.forward * 50f;
            previousVelocity = rb.linearVelocity;
        }

    private void Update()
    {
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            transform.position = new Vector3(1666f, 3f, 1000f);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            rb.linearVelocity = transform.forward * 40f;
            rb.angularVelocity = Vector3.zero;
        }
    }

    public void FixedUpdate()
    {
        Vector3 currentVelocity = rb.linearVelocity;
        acceleration = (currentVelocity - previousVelocity) / Time.fixedDeltaTime;
        previousVelocity = currentVelocity;

        if (rb.angularVelocity.magnitude > maxSafeAngularSpeed)
        {
            rb.angularVelocity = rb.angularVelocity.normalized * maxSafeAngularSpeed; 
        }
    }

}
