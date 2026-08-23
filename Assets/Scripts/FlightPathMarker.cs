using UnityEngine;

public class FlightPathMarker : MonoBehaviour
{
    [SerializeField] private RectTransform fpmIcon;
    [SerializeField] private RectTransform pitchCrosshair;
    [SerializeField] private PhysicsBody physicsBody;
    [SerializeField] private Camera mainCamera;
    
    [SerializeField] private float minSpeedToShow = 1f;

    void Update()
    {
        Vector3 velocity = physicsBody.GetVelocity();

        if (velocity.magnitude < minSpeedToShow)
        {
            fpmIcon.gameObject.SetActive(false);
            return;
        }

        fpmIcon.gameObject.SetActive(true);

        Vector3 flightDirectionPoint = physicsBody.GetPosition() + velocity.normalized * 100;
        Vector3 screenPointFPM = mainCamera.WorldToScreenPoint(flightDirectionPoint);
        fpmIcon.position = screenPointFPM + new Vector3(0, 1f, 0);
        if (screenPointFPM.z < 0)
        {
            fpmIcon.gameObject.SetActive(false);
            return;
        }

        Vector3 noseDirectionPoint = physicsBody.GetPosition() + physicsBody.GetForward() * 100;
        Vector3 screenPointCrosshair = mainCamera.WorldToScreenPoint(noseDirectionPoint);

        if (screenPointCrosshair.z < 0)
        {
            pitchCrosshair.gameObject.SetActive(false);
        }
        else
        {
            pitchCrosshair.gameObject.SetActive(true);
            pitchCrosshair.position = screenPointCrosshair;
        }
    }
}
