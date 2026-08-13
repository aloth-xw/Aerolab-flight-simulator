using UnityEngine;
public enum WingSide
{
    Left, Right
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

        float aoa = aerodynamics.GetAngleOfAttack(wingForward,wingUp);

        float cl = airfoil.GetLiftCoefficient(Mathf.Clamp(aoa, -20f, 20f));
        //float cl = 0.01f;
        float cd = airfoil.GetDragCoefficient(Mathf.Clamp(aoa, -20f, 20f));


        Vector3 relativeAirFlow = aerodynamics.GetRelativeAirFlow();

        float speed = relativeAirFlow.magnitude;

        float density = 1.225f;


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

        Vector3 liftDirection = aerodynamics.GetLiftDirection(wingUp);

        Vector3 dragDirection = aerodynamics.GetDragDirection();
        
        Debug.Log("AoA: " + aoa + " | CL: " + cl + " | Lift: " + lift);
        Debug.Log("Lift Direction: " + liftDirection +" | Drag Direction: " + dragDirection);
        Debug.Log("Speed: " + speed +" | AoA: " + aoa +" | CL: " + cl +" | Lift: " + lift
);
        aerodynamics.ApplyForce(liftDirection, lift, transform.position);
        aerodynamics.ApplyForce(dragDirection, drag, transform.position);
      
    }
}
