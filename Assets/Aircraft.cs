using UnityEngine;

public class Aircraft : MonoBehaviour
{
    private PhysicsBody physicsBody;
    private Aerodynamics aerodynamics;
    private Engine engine;

    private WingConfig[] aeroSurfaces;

    private void Awake()
    {
        physicsBody = GetComponent<PhysicsBody>();
        aerodynamics = GetComponent<Aerodynamics>();
        engine = GetComponent<Engine>();

        aeroSurfaces = GetComponentsInChildren<WingConfig>();
    }

    public float GetTotalDrag()
    {
        float totalDrag = 0f;

        foreach (WingConfig surface in aeroSurfaces)
        {
            totalDrag += surface.GetCurrentDrag();
        }

        return totalDrag;
    }

    public float GetSpeed()
    {
        return physicsBody.GetVelocity().magnitude;
    }

    public float GetThrottle()
    {
        return engine.GetThrottle();
    }

    public float GetAltitude()
    {
        return transform.position.y;
    }

    public float GetThrust()
    {
        return engine.GetCurrentThrust();
    }

    public float GetNetThrust()
    {
        return GetThrust() - GetTotalDrag();
    }

    public float GetForwardAcceleration()
    {
        return Vector3.Dot(physicsBody.GetAcceleration(),physicsBody.GetForward());
    }

    public void SetRollInput(float input)
    {
        foreach (WingConfig surface in aeroSurfaces)
        {
            if (surface.GetSurfaceType() != AeroSurfaceType.MainWing)
                continue;

                surface.SetAileronInput(input);
        }

    }

    public float GetAoA()
    {
       Vector3 relativeAirflow = aerodynamics.GetRelativeAirFlow(physicsBody.GetCenterOfMass());

       return aerodynamics.GetAngleOfAttack(transform.forward,transform.up,relativeAirflow);
    }

    private void FixedUpdate()
    {

    Debug.Log(
        "Speed: " + GetSpeed() +
        " | Throttle: " + engine.GetThrottle() +
        " | Thrust: " + GetThrust() +
        " | Drag: " + GetTotalDrag() +
        " | Net: " + GetNetThrust() +
        " | Forward Acc: " + GetForwardAcceleration()
    );

    }
}
