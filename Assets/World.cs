using UnityEngine;

public class World : MonoBehaviour
{
   [SerializeField]
   private Atmosphere atmosphere;

   [SerializeField]
   private Gravity gravity;

   public Atmosphere GetAtmosphere()
    {
        return atmosphere;
    }

    public Gravity GetGravity()
    {
        return gravity;
    }
}
