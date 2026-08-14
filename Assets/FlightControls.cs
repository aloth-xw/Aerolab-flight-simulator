using UnityEngine;
using UnityEngine.InputSystem;

public class FlightControls : MonoBehaviour
{
    
    private Engine engine;

    [SerializeField]
    private InputActionReference throttleAction;

    private float throttle;

    private void Awake()
    {
        engine = GetComponent<Engine>();
        throttleAction.action.Enable();
    }

    private void Update()
    {
      float throttleInput = throttleAction.action.ReadValue<float>();

      throttle += throttleInput * Time.deltaTime;
      throttle = Mathf.Clamp01(throttle);

      engine.SetThrottle(throttle);

      
      Debug.Log("Throttle Input: " + throttleInput + " Throttle: " + throttle);
    }
}
