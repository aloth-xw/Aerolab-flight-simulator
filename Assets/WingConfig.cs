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
    private float aileronEffect = 0.2f;

    [SerializeField]
    private float aileronInput = 0f;

    [SerializeField]
    private AeroSurfaceType surfaceType;

    private float currentDrag;
    private float currentAoA;

    public float GetCurrentAoA()
    {
        return currentAoA;
    }

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

    public float GetCurrentDrag()
    {
        return currentDrag;
    }

    public WingSide GetSide()
    {
        return side;
    }

    public AeroSurfaceType GetSurfaceType()
    {
        return surfaceType;
    }

    public void SetAileronInput(float input)
    {
        aileronInput = Mathf.Clamp(input, -1f, 1f);
    }

    
    private void FixedUpdate()
    {
        Aerodynamics aerodynamics = GetComponentInParent<Aerodynamics>();
        PhysicsBody physicsBody = GetComponentInParent<PhysicsBody>();

        Vector3 wingForward = transform.forward;
        Vector3 wingUp = transform.up;

        Vector3 relativeAirFlow = aerodynamics.GetRelativeAirFlow(transform.position);


        float aoa = aerodynamics.GetAngleOfAttack(wingForward,wingUp, relativeAirFlow);
        currentAoA = aoa;

        float cl = 0f;
        float cd = 0f;

        if (Mathf.Abs(aoa) <= 90f)
        {
            float aerodynamicAoA = Mathf.Clamp(aoa, -20f, 20f);

            cl = airfoil.GetLiftCoefficient(aerodynamicAoA);
            cd = airfoil.GetDragCoefficient(aerodynamicAoA);
        }
        else
        {
            cl = 0f;
            cd = airfoil.GetDragCoefficient(20f);
        }

        float speed = relativeAirFlow.magnitude;
        float density = aerodynamics.GetAirDensity();


        float lift = aerodynamics.CalculateLift(density,speed,area,cl);


        float liftMultiplier = 1f;
        
        if (surfaceType ==AeroSurfaceType.MainWing)
        {
            if (side == WingSide.Left)
            {
                liftMultiplier += aileronInput * aileronEffect;
            }
            else
            {
                liftMultiplier -= aileronInput * aileronEffect;
            }
        }
        lift *= liftMultiplier;

        currentDrag = aerodynamics.CalculateDrag(density,speed,area,cd);

        Vector3 liftDirection = aerodynamics.GetLiftDirection(wingUp, relativeAirFlow);

        Debug.Log(
    gameObject.name +
    " | Pos: " + transform.position +
    " | LiftDir: " + liftDirection +
    " | Lift: " + lift
);

        Vector3 dragDirection = aerodynamics.GetDragDirection(relativeAirFlow);

        aerodynamics.ApplyForce(liftDirection, lift, transform.position);
        aerodynamics.ApplyForce(dragDirection, currentDrag, transform.position);

        Debug.Log(gameObject.name + " | AoA: " + aoa + " | Lift: " + lift + " | Drag: " + currentDrag);
      
    }
}
