using UnityEngine;

public class LandingGear : MonoBehaviour
{
    [System.Serializable]
    public class GearPoint
    {
        public Transform point;
        [HideInInspector] public bool grounded;
    }

    [SerializeField] private PhysicsBody physicsBody;
    [SerializeField] private GearPoint noseGear;
    [SerializeField] private GearPoint leftMainGear;
    [SerializeField] private GearPoint rightMainGear;

    [SerializeField] private float suspensionRestLength = 1.5f;
    [SerializeField] private float suspensionStrength = 8000f;
    [SerializeField] private float suspensionDamping = 800f;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private float sideFrictionStrength = 3000f;
    [SerializeField] private float rollingFriction = 5f;

    public bool IsGrounded { get; private set; }

    private void FixedUpdate()
    {
        bool anyGrounded = false;
        anyGrounded |= ApplyGear(noseGear);
        anyGrounded |= ApplyGear(leftMainGear);
        anyGrounded |= ApplyGear(rightMainGear);
        IsGrounded = anyGrounded;
    }

    private bool ApplyGear(GearPoint gear)
    {
        if (gear.point == null) return false;

        if (Physics.Raycast(gear.point.position, -transform.up, out RaycastHit hit, suspensionRestLength, groundLayer))
        {
            float compression = suspensionRestLength - hit.distance;
            float springForce = compression * suspensionStrength;

            Vector3 pointVelocity = physicsBody.GetVelocity() + Vector3.Cross(physicsBody.GetAngularVelocity(),gear.point.position-physicsBody.GetCenterOfMass());

            float damperForce = -Vector3.Dot(pointVelocity, transform.up) * suspensionDamping;
            Vector3 suspensionForce = transform.up * (springForce + damperForce);
            physicsBody.AddForceAtPosition(suspensionForce, gear.point.position);

            Vector3 sideVelocity = Vector3.Project(pointVelocity, transform.right);
            physicsBody.AddForceAtPosition(-sideVelocity * sideFrictionStrength * Time.fixedDeltaTime, gear.point.position);

            Vector3 forwardVelocity = Vector3.Project(pointVelocity, transform.forward);
            physicsBody.AddForceAtPosition(-forwardVelocity * rollingFriction, gear.point.position);

            gear.grounded = true;
            return true;
        }
            gear.grounded = false;

            return false;
    }
}


