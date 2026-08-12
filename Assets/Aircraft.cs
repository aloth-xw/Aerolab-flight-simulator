using UnityEngine;

public class Aircraft : MonoBehaviour
{
    private PhysicsBody physicsBody;
    private Aerodynamics aerodynamics;

    private void Awake()
    {
        physicsBody = GetComponent<PhysicsBody>();
        aerodynamics = GetComponent<Aerodynamics>();
            }
}
