using UnityEngine;

[System.Serializable]
public class PIDController
{
    [SerializeField] private float kp = 300f;
    [SerializeField] private float ki = 0f;
    [SerializeField] private float kd = 0f;
    [SerializeField] private float integralClamp = 100f;
    
    private float integral = 0f;
    private float previousError = 0f;

    public float Update(float error, float deltaTime)
    {
        integral += error * deltaTime;
        integral = Mathf.Clamp(integral, -integralClamp, integralClamp);

        float derivative = (error - previousError) / Mathf.Max(deltaTime, 0.001f);
        previousError = error;

        return kp * error + ki * integral + kd * derivative;
    }

    public void Reset()
    {
        integral = 0f;
        previousError = 0f;
    }
}
