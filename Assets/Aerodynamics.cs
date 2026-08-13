using UnityEngine;

public class Aerodynamics : MonoBehaviour

{
    [SerializeField]
    private World world;
    private PhysicsBody physicsBody;


   private void Awake()
    {
        physicsBody = GetComponent<PhysicsBody>();
    }

    public float GetAirDensity()
    {
        return world.GetAtmosphere().GetDensity();
    }
    public Vector3 GetRelativeAirFlow(Vector3 WorldPosition)
    {
        Vector3 wind = world.GetAtmosphere().GetWind();

        Vector3 centreOfMass = physicsBody.GetCenterOfMass();
        Vector3 angularVelocity = physicsBody.GetAngularVelocity();

        Vector3 pointVelocity = physicsBody.GetVelocity() + Vector3.Cross(angularVelocity, WorldPosition-centreOfMass);
        return pointVelocity - wind;
    }

    public float GetAngleOfAttack(Vector3 wingForward, Vector3 wingUp, Vector3 relativeAirFlow)
    {
        if (relativeAirFlow.sqrMagnitude < 0.001f)
            return 0f;
            
        if (relativeAirFlow.magnitude < 1f)
        return 0f;

        Vector3 airflow = relativeAirFlow.normalized;


        float forwardComponent = Vector3.Dot(airflow,wingForward);
        float verticalComponent = Vector3.Dot(airflow,wingUp);

        return Mathf.Atan2(verticalComponent, forwardComponent)*Mathf.Rad2Deg;
    }

    public float CalculateLift(float density, float speed, float area, float cl)
    {
        return 0.5f * density * speed * speed * area * cl;
    }


    public Vector3 GetLiftDirection(Vector3 wingUp, Vector3 relativeAirFlow)
    {

        if (relativeAirFlow.sqrMagnitude < 0.0001f)
            return Vector3.zero;

        Vector3 flow = relativeAirFlow.normalized;
        Vector3 side = Vector3.Cross(wingUp, flow);

        return Vector3.Cross(flow,side).normalized;
    }

    public float CalculateDrag(float density, float speed, float area, float cd)
    {
        return 0.5f * density * speed * speed * area * cd; 
    }

    public Vector3 GetDragDirection(Vector3 relativeAirFlow)
    {
        if (relativeAirFlow.sqrMagnitude < 0.0001f)
            return Vector3.zero;

        return -relativeAirFlow.normalized; 
    }

    public void ApplyForce(Vector3 direction, float magnitude, Vector3 position)
    {
        physicsBody.AddForceAtPosition(direction * magnitude, position);
    }
}
