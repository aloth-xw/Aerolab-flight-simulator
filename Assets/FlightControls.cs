using UnityEngine;
using UnityEngine.InputSystem;

public class FlightControls : MonoBehaviour
{
    
    private Engine engine;

    private PhysicsBody physicsBody;

    [SerializeField]
    private InputActionReference throttleAction;

    [SerializeField]
    private InputActionReference rollAction;

    [SerializeField]
    private InputActionReference pitchAction;

    [SerializeField]
    private InputActionReference yawAction;

    [SerializeField]
    private float rollTorque = 100f;

    [SerializeField]
    private float pitchTorque = 10f;

    [SerializeField]
    private float yawTorque = 10f;

    private float throttle;
    private float throttleInput;
    private float rollInput;
    private float pitchInput;

    private float yawInput;

    private void Awake()
    {
        engine = GetComponent<Engine>();
        physicsBody = GetComponent<PhysicsBody>();
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
      pitchInput = pitchAction.action.ReadValue<float>();
      yawInput = yawAction.action.ReadValue<float>();

    }

    private void FixedUpdate()
    {
        physicsBody.AddTorque(physicsBody.GetForward()*rollInput*rollTorque);
        physicsBody.AddTorque(physicsBody.GetRight()*pitchInput*pitchTorque);
        physicsBody.AddTorque(physicsBody.GetUp()*yawInput*yawTorque);
    }
}
