using UnityEngine;

public class Atmosphere : MonoBehaviour
{
   [SerializeField]
   private Vector3 wind = new Vector3(0f, 0f, 0f);

   [SerializeField]
   private float density = 1.225f;

   public Vector3 GetWind()
    {
        return wind;
    } 

    public float GetDensity()
    {
        return density;
    }
}
