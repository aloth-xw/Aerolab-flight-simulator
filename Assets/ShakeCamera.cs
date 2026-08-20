using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Aircraft aircraft;

    [Header("Shake por AoA / Pérdida")]
    [SerializeField] private float stallAoAThreshold = 15f;
    [SerializeField] private float AoAShakeStartMargin = 5f;
    [SerializeField] private float maxAoAShakeIntensity = 0.3f;

    [Header("Shake por Velocidad")]
    [SerializeField] private float speedShakeThreshold = 100f;
    [SerializeField] private float speedShakeRange = 100f;
    [SerializeField] private float maxSpeedShakeIntensity = 0.25f;

    [Header("General")]
    [SerializeField] private float shakeFrequency = 25f;
    [SerializeField] private float positionShakeAmount = 0.15f;
    [SerializeField] private float rotationShakeAmount = 1.5f;

    private float noiseSeedX;
    private float noiseSeedY;
    private float noiseSeedZ;

    public Vector3 CurrentPositionOffset {get; private set; }
    public Quaternion CurrentRotationOffset {get; private set; } = Quaternion.identity;

    void Awake()
    {
        noiseSeedX = Random.Range(0f, 1000f);
        noiseSeedY = Random.Range(0f, 1000f);
        noiseSeedZ = Random.Range(0f, 1000f);
    }

    void Update()
    {
        if (aircraft == null)
        {
            CurrentPositionOffset=Vector3.zero;
            CurrentRotationOffset=Quaternion.identity;
            return;
        }

        float intensity = CalculateIntensity();

        if (intensity <= 0.001f)
        {
            CurrentPositionOffset=Vector3.zero;
            CurrentRotationOffset=Quaternion.identity;
            return;
        }

        float t = Time.time * shakeFrequency;

        float shakeX = (Mathf.PerlinNoise(noiseSeedX, t) - 0.5f) * 2f;
        float shakeY = (Mathf.PerlinNoise(noiseSeedX, t) - 0.5f) * 2f;
        float shakeZ = (Mathf.PerlinNoise(noiseSeedX, t) - 0.5f) * 2f;

        CurrentPositionOffset = new Vector3(shakeX, shakeY, 0f) * positionShakeAmount * intensity;

        CurrentRotationOffset = Quaternion.Euler(shakeY * rotationShakeAmount * intensity, 
        shakeX*rotationShakeAmount*intensity, 
        shakeZ*rotationShakeAmount*intensity*0.5f);
    }

    private float CalculateIntensity()
    {
        float AoAIntensity = 0f;
        float currentAoA = Mathf.Abs(aircraft.GetCurrentAoA());
        float AoAShakeStart = stallAoAThreshold - AoAShakeStartMargin;

        if (currentAoA > AoAShakeStart)
        {
            float t = Mathf.InverseLerp(AoAShakeStart, stallAoAThreshold, currentAoA);
            AoAIntensity = Mathf.Clamp01(t) * maxAoAShakeIntensity;

            if (currentAoA >= stallAoAThreshold)
                AoAIntensity = maxAoAShakeIntensity;
        }

         float speedIntensity = 0f;
        float currentSpeed = aircraft.GetSpeed();

        if (currentSpeed > speedShakeThreshold)
        {
            float t = Mathf.InverseLerp(speedShakeThreshold, speedShakeThreshold + speedShakeRange, currentSpeed);
            speedIntensity = Mathf.Clamp01(t) * maxSpeedShakeIntensity;
        }

        return AoAIntensity + speedIntensity;
    }
}
