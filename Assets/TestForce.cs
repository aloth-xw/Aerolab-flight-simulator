using UnityEngine;

public class TestForce : MonoBehaviour
{
    [SerializeField]
    private float force = 10f; 
    
    private PhysicsBody physicsBody;


    private void Awake()
    {
        physicsBody = GetComponent<PhysicsBody>();
    }
    private void FixedUpdate()
    {
        physicsBody.AddForce(physicsBody.GetForward() * force); 
    }
}
