using UnityEngine;

public class PitchLadder : MonoBehaviour
{
    [SerializeField] private Transform aircraft;
    [SerializeField] private RectTransform ladderContainer;
    [SerializeField] private PitchLadderRung[] rungs;
    [SerializeField] private float pixelsPerDegree = 8f;

    private void Update()
    {
        if (aircraft == null || ladderContainer == null) return;
        
        Vector3 forward = aircraft.forward;
        Vector3 up = aircraft.up;

        float pitchAngle = Mathf.Asin(Mathf.Clamp(forward.y, -1f, 1f)) * Mathf.Rad2Deg;

        Vector3 worldUpProjected = Vector3.ProjectOnPlane(Vector3.up, forward).normalized;
        float rollAngle = Vector3.SignedAngle(worldUpProjected, up, forward);

        ladderContainer.localRotation = Quaternion.Euler(0, 0, -rollAngle);

        foreach (PitchLadderRung rung in rungs)
        {
            RectTransform rt =rung.GetComponent<RectTransform>();
            float offset = (rung.Angle -pitchAngle) * pixelsPerDegree;
            rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, offset);
        }
    }
}
