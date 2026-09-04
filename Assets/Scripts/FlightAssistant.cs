using UnityEngine;
using TMPro;

public class FlightAssistant : MonoBehaviour
{
    [SerializeField] private Aircraft aircraft;
    [SerializeField] private PhysicsBody physicsBody;
    [SerializeField] private TMP_Text assistantText;

    [SerializeField] private float stallAoAThreshold = 15f;
    [SerializeField] private float preStallMargin = 4f;

    [SerializeField] private float terrainWarningAltitude = 100f;
    [SerializeField] private float dangerousDescentRate = -15f;

    [SerializeField] private float speedThreshold = 130f;

    [SerializeField] private float messageCooldown = 2f;
    [SerializeField] private float warningConfirmTime = 1f;
    private float idleTimer = 0f;

    private string currentWarning = "";
    private float coolDownTimer = 0f;

    private void Update()
    {
        if (aircraft == null || physicsBody == null) return;

        coolDownTimer -= Time.deltaTime;

        string newWarning = EvaluateWarnings();

        if (newWarning != currentWarning)
        {
            currentWarning = newWarning;
            if (assistantText != null)
                assistantText.text = currentWarning;

            if (!string.IsNullOrEmpty(currentWarning) && coolDownTimer <= 0f)
            {
                coolDownTimer = messageCooldown;
            }
        }
    }

    private string EvaluateWarnings()
    {
        float aoa = Mathf.Abs(aircraft.GetCurrentAoA());
        float speed = aircraft.GetSpeed();
        float altitude = aircraft.GetAltitude();
        float verticalSpeed = physicsBody.GetVelocity().y;
        float throttle = aircraft.GetThrottle();

        if (altitude < terrainWarningAltitude && verticalSpeed<dangerousDescentRate)
        {
            return "¡ALTURA BAJA! ¡SUBE EL MORRO!";
        }

        if (aoa >= stallAoAThreshold)
        {
            return "¡PÉRDIDA! ¡BAJA EL MORRO!";
        }

        if (aoa >= stallAoAThreshold - preStallMargin)
        {
            return "Ángulo de ataque alto";
        }

        if (speed > speedThreshold)
        {
            return "Velocidad excesiva";
        }

        if (throttle < 0.05f && altitude > 20f)
        {
            idleTimer += Time.deltaTime;
            if (idleTimer >= warningConfirmTime)
                return "Motor al ralentí";
        }
        else
        {
            idleTimer = 0f;
        }

        return "";
    }

}
