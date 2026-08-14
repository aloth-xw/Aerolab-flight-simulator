using UnityEngine;

public class Engine : MonoBehaviour
{
  [SerializeField]
  private float maxThrust = 5000f;

  [SerializeField]
  [Range(0f, 1f)]
  private float throttle = 1f;

  private PhysicsBody physicsBody;

  private void Awake()
    {
        physicsBody = GetComponent<PhysicsBody>();
    }

    public void SetThrottle(float value)
    {
        throttle = Mathf.Clamp01(value);
    }

 private void FixedUpdate()
    {
        float currentThrust = maxThrust*throttle;
        physicsBody.AddForce(physicsBody.GetForward()*currentThrust);
    }

}
