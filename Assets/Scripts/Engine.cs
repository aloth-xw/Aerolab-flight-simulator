using UnityEngine;

public class Engine : MonoBehaviour
{
  [SerializeField]
  private float maxThrust = 20000f;

  [SerializeField]
  [Range(0f, 1f)]
  private float throttle = 1f;

  [SerializeField] private float maxFuel = 100f;
  [SerializeField] private float fuelConsumptionRate = 1f;

  private float currentFuel;

  private PhysicsBody physicsBody;

  private void Awake()
    {
        physicsBody = GetComponent<PhysicsBody>();
        currentFuel = maxFuel;
    }

    public float GetFuel()
    {
        return currentFuel;
    }

    public float GetFuelPercent()
    {
        return currentFuel / maxFuel;
    }

    public void SetThrottle(float value)
    {
        throttle = Mathf.Clamp01(value);
    }

    public float GetCurrentThrust()
    {
        return maxThrust*throttle;
    }

    public float GetThrottle()
    {
        return throttle;
    }

 private void FixedUpdate()
    {
        float currentThrust = maxThrust*throttle;
        physicsBody.AddForce(physicsBody.GetForward()*currentThrust);

        currentFuel -= fuelConsumptionRate * throttle * Time.fixedDeltaTime;
        currentFuel = Mathf.Clamp(currentFuel, 0f, maxFuel);
    }

}
