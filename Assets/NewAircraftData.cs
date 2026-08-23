using UnityEngine;

[CreateAssetMenu(fileName="NewAircraftData",menuName="Aerolab/Aircraft Data")]

public class NewAircraftData: ScriptableObject
{
    public string aircraftName ="F/A-18 Super Hornet";
    public GameObject prefab3D;

    [Range(0f,1f)] public float topSpeed = 0.85f;
    [Range(0f,1f)] public float handling = 0.70f;
    [Range(0f,1f)] public float acceleration = 0.90f;

    public string trackBestTime="03:35 m/s";
}
