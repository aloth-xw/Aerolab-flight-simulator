using UnityEngine;
using UnityEngine.InputSystem;

public class FlightControls : MonoBehaviour
{
    
    private Engine engine;
    private PhysicsBody physicsBody;
    private Aircraft aircraft;


    [SerializeField]
    private InputActionReference throttleAction;

    [SerializeField]
    private InputActionReference rollAction;

    [SerializeField]
    private InputActionReference pitchAction;

    [SerializeField]
    private InputActionReference yawAction;


    [SerializeField]
    private float maxRollRate = 60f;

    [SerializeField]
    private float rollControlStrength = 100f;

 

    [SerializeField]
    private float maxPitchRate = 60f;

    [SerializeField]
    private float pitchControlStrength = 100f;


    [SerializeField]
    private float maxYawRate = 60f;
    [SerializeField]
    private float yawControlStrength = 100f;

    private float throttle;
    private float throttleInput;

    private float rollInput;
    private float rollTorque;

    private float pitchInput;
    private float pitchTorque;

    private float yawInput;
    private float yawTorque;

    private void Awake()
    {
        engine = GetComponent<Engine>();
        physicsBody = GetComponent<PhysicsBody>();
        aircraft = GetComponent<Aircraft>();
    }

    private void OnEnable()
    {
        throttleAction.action.Enable();
        rollAction.action.Enable();
        pitchAction.action.Enable();
        yawAction.action.Enable();
    }

    private void OnDisable()
    {
        throttleAction.action.Disable();
        rollAction.action.Disable();
        pitchAction.action.Disable();
        yawAction.action.Disable();
    }

    private void Update()
    {
      throttleInput = throttleAction.action.ReadValue<float>();

      throttle += throttleInput * Time.deltaTime;
      throttle = Mathf.Clamp01(throttle);

      engine.SetThrottle(throttle);
      
  
      rollInput = rollAction.action.ReadValue<float>();
      aircraft.SetRollInput(rollInput);
      pitchInput = pitchAction.action.ReadValue<float>();
      yawInput = yawAction.action.ReadValue<float>();

      float targetRollRate = rollInput * maxRollRate;
      float currentRollRate = Vector3.Dot(physicsBody.GetAngularVelocity(),physicsBody.GetForward())* Mathf.Rad2Deg;
      float rollError = targetRollRate - currentRollRate;
      rollTorque = rollError * rollControlStrength;

      float targetPitchRate = pitchInput * maxPitchRate;
      float currentPitchRate = Vector3.Dot(physicsBody.GetAngularVelocity(),physicsBody.GetRight())*Mathf.Rad2Deg;
      float pitchError = targetPitchRate - currentPitchRate;
      pitchTorque = pitchError * pitchControlStrength;

      float targetYawRate = yawInput * maxYawRate;
      float currentYawRate = Vector3.Dot(physicsBody.GetAngularVelocity(),physicsBody.GetUp())*Mathf.Rad2Deg;
      float yawError = targetYawRate - currentYawRate;
      yawTorque = yawError * yawControlStrength;
    }

    private void FixedUpdate()
    {
        //physicsBody.AddTorque(physicsBody.GetForward()*rollTorque);
        physicsBody.AddTorque(physicsBody.GetRight()*pitchTorque);
        physicsBody.AddTorque(physicsBody.GetUp()*yawTorque);
    }
}
