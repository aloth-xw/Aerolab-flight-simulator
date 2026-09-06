using UnityEngine;
using UnityEngine.InputSystem;

public class FlightControls : MonoBehaviour
{
    //VALORES ACTUALES (21 agosto)
    //pitchControlStrength: 300
    //aoaCorrectionSTrength: 50
    // yawControlStrength: 300
    // Inertia Tensor: (5417, 13667, 8417)
    // Angulo alas: +3°, cola: -2°
    // maxSafeAngularSpeed: 3
    
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
    private float controlResponse = 5f;



    //[SerializeField] private float maxPitchRate = 60f;

   // [SerializeField] private float pitchControlStrength = 100f;


    [SerializeField]
    private float maxYawRate = 60f;
    [SerializeField]
    private float yawControlStrength = 100f;

   // [SerializeField] private float targetAoA = 5f;
    //[SerializeField] private float aoaCorrectionStrength = 15f;

    [SerializeField] private LandingGear landingGear;
    [SerializeField] private float groundSteerStrength = 3000f;

    [Header("Ptch - rate Controller")]
    [SerializeField] private float maxPitchRate = 40f;
    [SerializeField] private PIDController pitchRatePID;

    [Header("Pitch - Autonivelado")]
    [SerializeField] private float levelHoldStrength = 2f;
    [SerializeField] private float inputDeadZone = 0.05f;

    [Header("Pitch - protección de pérdida")]
    [SerializeField] private float aoaSafetyThreshold = 12f;
    [SerializeField] private float aoaSafetyStrength = 3f;

    private float throttle;
    private float throttleInput;

    private float rollInput;

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

    private void UpdatePitch(float deltaTime)
    {
        float currentPitchAngle = Mathf.Asin(Mathf.Clamp(transform.forward.y, -1f, 1f)) * Mathf.Rad2Deg;
        float currentPitchRate = Vector3.Dot(physicsBody.GetAngularVelocity(), physicsBody.GetRight()) * Mathf.Rad2Deg;

        float targetPitchRate;

        if (Mathf.Abs(pitchInput) > inputDeadZone)
        {
            targetPitchRate = pitchInput * maxPitchRate;
        }
        else
        {
            targetPitchRate = -currentPitchAngle * levelHoldStrength;
        }

        float rateError = targetPitchRate - currentPitchRate;
        float pitchTorque = pitchRatePID.Update(rateError, deltaTime);

        float currentAoA = aircraft.GetCurrentAoA();
        if (currentAoA > aoaSafetyThreshold)
        {
            float excess = currentAoA - aoaSafetyThreshold;
            pitchTorque -= excess * aoaSafetyStrength;
        }

        Debug.Log("currentAoA: " + currentAoA + " | pitchTorque final: " + pitchTorque);
        Debug.Log("pitchInput: " + pitchInput + " | targetRate: " + targetPitchRate + " | currentRate: " + currentPitchRate + " | rateError: " + rateError + " | pitchTorque (PID): " + pitchTorque);
        Debug.Log("Torque aplicado: " + pitchTorque + " | AngularVel tras aplicar: " + physicsBody.GetAngularVelocity());

        this.pitchTorque = pitchTorque;
    }
    

    private void Update()
    {
      throttleInput = throttleAction.action.ReadValue<float>();

      throttle += throttleInput * Time.deltaTime;
      throttle = Mathf.Clamp01(throttle);

      engine.SetThrottle(throttle);
      

      float targetRollInput = rollAction.action.ReadValue<float>();
      float targetPitchInput = pitchAction.action.ReadValue<float>();
      float targetYawInput = yawAction.action.ReadValue<float>();

      rollInput = Mathf.MoveTowards(rollInput,targetRollInput, controlResponse*Time.deltaTime);
      pitchInput = Mathf.MoveTowards(pitchInput,targetPitchInput, controlResponse*Time.deltaTime);
      yawInput = Mathf.MoveTowards(yawInput,targetYawInput, controlResponse*Time.deltaTime);

      aircraft.SetRollInput(rollInput);


        /*float targetPitchRate = pitchInput * maxPitchRate;
        float currentPitchRate = Vector3.Dot(physicsBody.GetAngularVelocity(),physicsBody.GetRight())*Mathf.Rad2Deg;
        float pitchError = targetPitchRate - currentPitchRate;

        float aoaError = targetAoA - aircraft.GetCurrentAoA();
        float aoaCorrectionTorque = aoaError * aoaCorrectionStrength;

        pitchTorque = (pitchError * pitchControlStrength) + aoaCorrectionTorque;
        pitchTorque = Mathf.Clamp(pitchTorque, -20000f, 20000f);
        float maxPitchAngularSpeed = 1.5f;*/

        UpdatePitch(Time.deltaTime);
      

      if (landingGear != null && landingGear.IsGrounded)
        {
            yawTorque = yawInput * groundSteerStrength;
        }
      else
        {
            float targetYawRate = yawInput * maxYawRate;
            float currentYawRate = Vector3.Dot(physicsBody.GetAngularVelocity(),physicsBody.GetUp())*Mathf.Rad2Deg;
            float yawError = targetYawRate - currentYawRate;
            yawTorque = yawError * yawControlStrength;  
        }
        

      if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            transform.position = Vector3.zero + Vector3.up * 50f;
            transform.rotation = Quaternion.identity;
            GetComponent<Rigidbody>().linearVelocity = transform.forward * 40f;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }

    }

    private void FixedUpdate()
    {
        physicsBody.AddTorque(-physicsBody.GetRight()*pitchTorque);
        physicsBody.AddTorque(physicsBody.GetUp()*yawTorque);
    }
}
