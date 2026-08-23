using UnityEngine;

public class AircraftConfig : MonoBehaviour
{
    [SerializeField]
    private float emptyMass = 1000f;

    public float GetEmptyMass()
    {
        return emptyMass;
    }
}
