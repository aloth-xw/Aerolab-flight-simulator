using UnityEngine;
public enum WingSide
{
    Left, Right
}

public enum AeroSurfaceType
{
    MainWing, Tail
}
public class WingConfig : MonoBehaviour
{
    [SerializeField]
    private float area = 10f;

    [SerializeField]
    private Vector3 position;

    [SerializeField]
    private Vector3 orientation;

    [SerializeField]
    private AirfoilConfig airfoil;

    [SerializeField]
    private WingSide side;

    [SerializeField]
    private float aileronEffect = 1f;

    [SerializeField]
    private float aileronInput = 0f;

    [SerializeField]
    private AeroSurfaceType surfaceType;

    public float GetArea()
    {
        return area;
    }

    public Vector3 GetPosition()
    {
        return position;
    }

    public Vector3 GetOrientation()
    {
        return orientation;
    }

    public AirfoilConfig GetAirfoil()
    {
        return airfoil;
    }

    
    private void FixedUpdate()
    {
        Aerodynamics aerodynamics = GetComponentInParent<Aerodynamics>();

        Vector3 wingForward = transform.forward;
        Vector3 wingUp = transform.up;

        Vector3 relativeAirFlow = aerodynamics.GetRelativeAirFlow(transform.position);


        float aoa = aerodynamics.GetAngleOfAttack(wingForward,wingUp, relativeAirFlow);

        float cl = airfoil.GetLiftCoefficient(Mathf.Clamp(aoa, -20f, 20f));
        float cd = airfoil.GetDragCoefficient(Mathf.Clamp(aoa, -20f, 20f));

        float speed = relativeAirFlow.magnitude;
        float density = aerodynamics.GetAirDensity();


        float lift = aerodynamics.CalculateLift(density,speed,area,cl);

        float liftMultiplier = 1f;

        if (side == WingSide.Left)
        {
            liftMultiplier += aileronInput * aileronEffect;
        }
        else
        {
            liftMultiplier -= aileronInput * aileronEffect;
        }

        lift *= liftMultiplier;

        float drag = aerodynamics.CalculateDrag(density,speed,area,cd);

        Vector3 liftDirection = aerodynamics.GetLiftDirection(wingUp, relativeAirFlow);

        Vector3 dragDirection = aerodynamics.GetDragDirection(relativeAirFlow);
        
        Debug.Log("AoA: " + aoa + " | CL: " + cl + " | Lift: " + lift + " | AngularVelocity: " + aerodynamics.GetComponent<PhysicsBody>().GetAngularVelocity());

        aerodynamics.ApplyForce(liftDirection, lift, transform.position);
        aerodynamics.ApplyForce(dragDirection, drag, transform.position);
      
    }
}
