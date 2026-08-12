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

    public Vector3 GetRelativeAirFlow()
    {
        Vector3 aircraftVelocity = physicsBody.GetVelocity();
        Vector3 wind = world.GetAtmosphere().GetWind();
        return aircraftVelocity - wind;
    }

    public float GetAngleOfAttack(Vector3 wingForward, Vector3 wingUp)
    {
        Vector3 airflow = -GetRelativeAirFlow();

        if (airflow.sqrMagnitude < 0.0001f)
            return 0f;

        airflow.Normalize();


        float forwardComponent = Vector3.Dot(airflow,wingForward);
        float verticalComponent = Vector3.Dot(airflow,wingUp);

        return Mathf.Atan2(verticalComponent, forwardComponent)*Mathf.Rad2Deg;
    }

    public float CalculateLift(float density, float speed, float area, float cl)
    {
        return 0.5f * density * speed * speed * area * cl;
    }


    public Vector3 GetLiftDirection(Vector3 wingUp )
    {
        Vector3 relativeAirFlow = GetRelativeAirFlow();

        if (relativeAirFlow.sqrMagnitude < 0.0001f)
            return Vector3.zero;

        relativeAirFlow.Normalize();

        Vector3 side = Vector3.Cross(wingUp, relativeAirFlow);

        return Vector3.Cross(relativeAirFlow,side).normalized;
    }

    public float CalculateDrag(float density, float speed, float area, float cd)
    {
        return 0.5f * density * speed * speed * area * cd; 
    }

    public Vector3 GetDragDirection()
    {
        Vector3 relativeAirFlow = GetRelativeAirFlow();

        if (relativeAirFlow.sqrMagnitude <0.0001f)
         return Vector3.zero;

        return -relativeAirFlow.normalized; 
    }

    public void ApplyForce(Vector3 direction, float magnitude)
    {
        physicsBody.AddForce(direction * magnitude);
    }

    private void FixedUpdate()
    {
        Vector3 relativeAirFlow = GetRelativeAirFlow();
    }
}
