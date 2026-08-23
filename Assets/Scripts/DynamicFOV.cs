using UnityEngine;

public class DynamicFOV : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Aircraft aircraft;

    [SerializeField] private float baseFOV = 60f;
    [SerializeField] private float maxFOV = 67f;
    [SerializeField] private float minSpeed = 30f;
    [SerializeField] private float maxSpeed = 200f;
    [SerializeField] private float changeSpeed = 3f;

    [SerializeField] private AnimationCurve speedResponseCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);


    private void Awake()
    {
        if (mainCamera == null) mainCamera = GetComponent<Camera>();
    }

    private void Update()
    {
        if (mainCamera ==null || aircraft == null) return;

        float currentSpeed = aircraft.GetSpeed();
        float speedPercent = Mathf.InverseLerp(minSpeed, maxSpeed, currentSpeed);
        float curvedPercent = speedResponseCurve.Evaluate(speedPercent);
        float targetFOV = Mathf.Lerp(baseFOV, maxFOV, curvedPercent);

        float throttleBonus = (aircraft.GetThrottle() > 0.9f)? 3f : 0f;
        float finalTargetFOV = targetFOV + throttleBonus;
        mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, targetFOV, Time.deltaTime * changeSpeed);
    }
}

